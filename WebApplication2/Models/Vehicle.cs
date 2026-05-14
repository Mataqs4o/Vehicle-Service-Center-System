using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class Vehicle
{
    public int VehicleId { get; set; }

    [Required, StringLength(50)]
    public string Make { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Model { get; set; } = string.Empty;

    [Range(1900, 2100)]
    public int Year { get; set; }

    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public ICollection<ServiceRecord> ServiceRecords { get; set; } = new List<ServiceRecord>();
}
