using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.Web.Validation
{
    
    public class NotEqualAttribute : ValidationAttribute
    {
        private readonly string _otherProperty;

        public NotEqualAttribute(string otherProperty) : base("The two values cannot be the same.")
        {
            _otherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherProperty);

            if (otherPropertyInfo == null)
            {
                return new ValidationResult($"Property with the name {_otherProperty} not found.");
            }

            var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);

            if (object.Equals(value, otherValue))
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }

}
