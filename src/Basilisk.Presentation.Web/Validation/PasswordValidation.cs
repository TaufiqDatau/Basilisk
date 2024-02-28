using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Basilisk.Presentation.Web.Validation
{
    public class PasswordValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string password)
            {
                // Minimum length check
                if (password.Length < 6)
                {
                    return new ValidationResult("Password must be at least 6 characters long.");
                }

           
                if (!Regex.IsMatch(password, @"^(?=.*[a-z])"))
                {
                    return new ValidationResult("Password must contain lowercase letters.");
                }
                if(!Regex.IsMatch(password,@"^(?=.*[A-Z])"))
                {
                    return new ValidationResult("Password must contain uppercase letters");
                }
                if(!Regex.IsMatch(password, @"^(?=.*[0-9])"))
                {
                    return new ValidationResult("Password must contain number");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid data type for password.");

        }
    }
}
