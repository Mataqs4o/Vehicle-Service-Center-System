using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class ServiceRecord
{
    public int ServiceRecordId { get; set; }

    [DataType(DataType.Date)]
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public int VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }

    [Required, StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, 100000)]
    public decimal Cost { get; set; }
}
