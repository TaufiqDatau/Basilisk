using Basilisk.DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;
using Basilisk.Presentation.Web.Validation;
using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.Web.ViewModels.AuthenticationVM
{
    public class UserRegistrationViewModel
    {
        [UniqueUsername]
        public string Username { get; set; }
        [PasswordValidation]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="Password tidak sama")]
        public string RetypePassword { get; set; }
        public RoleEnum Role { get; set; }
        public List<SelectListItem>? Roles { get; set; }
    }
}
