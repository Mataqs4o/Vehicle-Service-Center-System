using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class Mechanic
{
    public int MechanicId { get; set; }

    [Required, StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string Specialization { get; set; } = string.Empty;
}
