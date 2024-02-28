using Basilisk.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.Web.Validation
{
    public class UniqueUsername : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var Username = (string)value;
            var dbContext = (BasiliskTfContext)validationContext.GetService(typeof(BasiliskTfContext));
            if (Username == null)
            {
                return new ValidationResult("Username must be filled");
            }
            if(dbContext.Accounts.Any(a=> a.Username==Username))
            {
                return new ValidationResult("Username already taken");
            }

            return ValidationResult.Success;
        }
    }
}
