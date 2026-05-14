using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Repositories;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers;

public class MechanicsController(IGenericRepository<Mechanic> mechanicRepository) : Controller
{
    [HttpGet]
    public IActionResult Create() => View(new MechanicSignupViewModel());

    [HttpPost]
    public async Task<IActionResult> Create(MechanicSignupViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        await mechanicRepository.AddAsync(new Mechanic
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Specialization = model.Specialization
        });

        TempData["Success"] = "You are now registered as a mechanic.";
        return RedirectToAction(nameof(Create));
    }
}
