using System.ComponentModel.DataAnnotations;
using Basilisk.Presentation.Web.ViewModels.ProductVM;
namespace Basilisk.Presentation.Web.Validation
{
    public class ReorderableAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int onOrder)
            {
                var product = (ProductViewModel)validationContext.ObjectInstance; //Mendapatkan dirinya sendiri


                if(product.OnOrder>0 && product.Discontinue)
                {
                    return new ValidationResult("You cannot order when it is discontinued");
                }

                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("This validation is only available for numbers");
            }
            return base.IsValid(value, validationContext);
        }
    }
}
