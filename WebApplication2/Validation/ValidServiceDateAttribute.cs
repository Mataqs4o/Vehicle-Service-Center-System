using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Validation;

public sealed class ValidServiceDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not DateTime date)
        {
            return new ValidationResult("Service date is required.");
        }

        if (date.Date < DateTime.UtcNow.Date)
        {
            return new ValidationResult("Service date cannot be in the past.");
        }

        return ValidationResult.Success;
    }
}
