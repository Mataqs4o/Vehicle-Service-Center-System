using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Services;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers;

public class ServiceAppointmentsController(IServiceRecordService serviceRecordService, ApplicationDbContext dbContext) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new ServiceAppointmentViewModel
        {
            AvailableVehicles = await GetVehicleOptionsAsync(),
            PriceEstimates = await serviceRecordService.GetPriceEstimatesAsync()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ServiceAppointmentViewModel model)
    {
        model.AvailableVehicles = await GetVehicleOptionsAsync();
        model.PriceEstimates = await serviceRecordService.GetPriceEstimatesAsync();
        if (!ModelState.IsValid) return View(model);

        var vehicleExists = await dbContext.Vehicles.AnyAsync(v => v.VehicleId == model.VehicleId);
        if (!vehicleExists)
        {
            ModelState.AddModelError(nameof(model.VehicleId), "Please select a valid vehicle.");
            return View(model);
        }

        try
        {
            await serviceRecordService.BookAppointmentAsync(model, User.Identity?.Name ?? "customer@example.com");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "We could not save your booking. Please try again.");
            return View(model);
        }

        TempData["Success"] = "Appointment booked successfully. Our team will contact you shortly.";
        return RedirectToAction(nameof(Create));
    }

    private async Task<List<SelectListItem>> GetVehicleOptionsAsync()
    {
        return await dbContext.Vehicles
            .OrderBy(v => v.Make)
            .ThenBy(v => v.Model)
            .Select(v => new SelectListItem
            {
                Value = v.VehicleId.ToString(),
                Text = $"#{v.VehicleId} - {v.Year} {v.Make} {v.Model}"
            })
            .ToListAsync();
    }
}
