using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.ViewModels.CategoryVM;
using System.ComponentModel.DataAnnotations; //Pembuatan Attribute

namespace Basilisk.Presentation.Web.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueCategoryNameAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var categoryName = (string)value;
            var dbContext = (BasiliskTfContext) validationContext.GetService(typeof(BasiliskTfContext));
            var category = (CategoryViewModel)validationContext.ObjectInstance;
            var nameExist = dbContext.Categories.Any(c => c.Name.Equals(categoryName));

            if(nameExist && category.Id==0)
            {
                return new ValidationResult("Category Name is already taken");
            }
            else
            {
                
                return ValidationResult.Success;
            }
            return base.IsValid(value, validationContext);
        }
        //public override bool IsValid(object? value) // Nilai yang disimpan oleh properti 
        //{

        //    return base.IsValid(value);
        //}
    }
}
