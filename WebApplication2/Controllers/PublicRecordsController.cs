using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers;

public class PublicRecordsController(ApplicationDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        var records = await BuildRecordsAsync();
        return View(records);
    }

    [HttpGet]
    public async Task<IActionResult> Latest()
    {
        var records = await BuildRecordsAsync();
        return Json(records);
    }

    private Task<List<PublicServiceRecordViewModel>> BuildRecordsAsync() => dbContext.ServiceRecords
        .Where(r => r.IsPublic)
        .Include(r => r.Vehicle)
        .OrderByDescending(r => r.Date)
        .Select(r => new PublicServiceRecordViewModel
        {
            ServiceRecordId = r.ServiceRecordId,
            Date = r.Date,
            Description = r.Description,
            Cost = r.Cost,
            VehicleLabel = r.Vehicle == null ? $"Vehicle #{r.VehicleId}" : $"{r.Vehicle.Year} {r.Vehicle.Make} {r.Vehicle.Model}"
        })
        .ToListAsync();
}
