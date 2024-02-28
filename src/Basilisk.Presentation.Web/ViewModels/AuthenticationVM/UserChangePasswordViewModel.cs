using System.ComponentModel.DataAnnotations;
using Basilisk.Presentation.Web.Validation;

namespace Basilisk.Presentation.Web.ViewModels.AuthenticationVM
{
    public class UserChangePasswordViewModel
    {
        public string Username { get; set; }

        [Required]
        [PasswordCorrect]
        public string OldPassword { get; set; }

        [Required]
        [PasswordValidation]
        [NotEqual("OldPassword", ErrorMessage ="The new Password cannot be the same as the old one")]
        public string NewPassword { get; set; }
        [Compare("NewPassword",ErrorMessage ="Password didn't match")]
        public string ConfirmNewPassword { get; set; }


    }
}
