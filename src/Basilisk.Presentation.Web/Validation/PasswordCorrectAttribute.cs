using Basilisk.Busines.Interface;
using Basilisk.Presentation.Web.Services;
using System.ComponentModel.DataAnnotations;


namespace Basilisk.Presentation.Web.Validation
{
    public class PasswordCorrectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var newPassword = (string)value;
            var usernameProperty = validationContext.ObjectType.GetProperty("Username");

            if (usernameProperty == null)
            {
                return new ValidationResult("Property Username not found in this context");
            }

            var services = (IAccountRepository)validationContext.GetService(typeof(IAccountRepository));
            var username = usernameProperty.GetValue(validationContext.ObjectInstance);

            if (username == null)
            {
                return new ValidationResult("Username is null in this context");
            }

            var user = services?.Get(username.ToString());

            if (user == null)
            {
                return new ValidationResult("User not found");
            }

            bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(newPassword, user.Password);

            if (!isCorrectPassword)
            {
                return new ValidationResult("The old password is incorrect");
            }

            return ValidationResult.Success;
        }

    }
}
