using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels;

public class MechanicSignupViewModel
{
    [Required, StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string Specialization { get; set; } = string.Empty;
}
