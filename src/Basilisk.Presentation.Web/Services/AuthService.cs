using Basilisk.Busines.Interface;
using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.ViewModels.AuthenticationVM;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Basilisk.Presentation.Web.Helper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Basilisk.DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Basilisk.Presentation.Web.Services
{
    public class AuthService
    {
        private readonly IAccountRepository _accountRepository;

        public AuthService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Register(UserRegistrationViewModel vm)
        {

            if (vm.Password != vm.RetypePassword)
            {
                throw new Exception("Password and confirmation password is not matching");
            }

            var newAccount = new Account
            {
                Username = vm.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(vm.Password),
                Role = vm.Role
            };

            _accountRepository.RegisterAccount(newAccount);

        }

        public AuthenticationTicket Login(UserLoginViewModel loginData)
        {
            var userData = _accountRepository.Get(loginData.Username);

            VerifyPassword(loginData.Password, userData.Password);

            var claims = new List<Claim>{
                new Claim("username", userData.Username),
                new Claim(ClaimTypes.Role, userData.Role.ToString())
                };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var authenticationProperties = new AuthenticationProperties
            {
                IssuedUtc = DateTime.Now,
                ExpiresUtc = DateTime.Now.AddMinutes(30)
            };

            return new AuthenticationTicket(principal, authenticationProperties, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private void VerifyPassword(string enteredPassword, string storedPassword)
        {
            bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(enteredPassword, storedPassword);

            if (!isCorrectPassword)
            {
                throw new PasswordException("Username or password is incorrect");
            }
        }

        public List<SelectListItem> GetRoles()
        {
            List<SelectListItem> roleList = new List<SelectListItem>();
            List<RoleEnum> roles = Enum.GetValues(typeof(RoleEnum)).Cast<RoleEnum>().ToList();
            foreach (RoleEnum role in roles)
            {
                roleList.Add(new SelectListItem
                {
                    Text = role.ToString(),
                    Value = role.ToString()
                });
            }
            return roleList;
        }

        public void ChangePassword(UserChangePasswordViewModel vm)
        {
            var user = _accountRepository.Get(vm.Username);
            if (!BCrypt.Net.BCrypt.Verify(vm.OldPassword, user.Password))
            {
                throw new KeyNotFoundException("Old password is incorrect");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(vm.NewPassword);

            _accountRepository.UpdatePassword(user);
        }

    }
}
