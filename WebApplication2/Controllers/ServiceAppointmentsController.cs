using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers;

[Authorize(Roles = "Customer,Administrator")]
public class ServiceAppointmentsController(IServiceRecordService serviceRecordService) : Controller
{
    [HttpGet]
    public IActionResult Create() => View(new ServiceAppointmentViewModel());

    [HttpPost]
    public async Task<IActionResult> Create(ServiceAppointmentViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        await serviceRecordService.BookAppointmentAsync(model, User.Identity?.Name ?? "customer@example.com");
        TempData["Success"] = "Appointment booked successfully.";
        return RedirectToAction(nameof(Create));
    }
}
