using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Validation;

namespace WebApplication2.ViewModels;

public class ServiceAppointmentViewModel
{
    [Required]
    public int VehicleId { get; set; }

    [ValidServiceDate]
    public DateTime Date { get; set; } = DateTime.UtcNow.Date.AddDays(1);

    [Required, StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, 100000)]
    public decimal EstimatedCost { get; set; }

    public List<SelectListItem> AvailableVehicles { get; set; } = [];
    public IReadOnlyList<ServicePriceEstimateViewModel> PriceEstimates { get; set; } = [];
}
