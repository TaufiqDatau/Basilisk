using Basilisk.Presentation.Web.Services;
using Basilisk.Presentation.Web.ViewModels.AuthenticationVM;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Basilisk.Presentation.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly AuthService _authService;
        public AuthenticationController(AuthService authService)
        {
            _authService = authService;
        }


        [HttpGet("Login")]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("HomePage", "Home");
            }
            return View();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginViewModel vm)
        {
            
            try
            {

                var ticket = _authService.Login(vm);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                            ticket.Principal, ticket.Properties);
                return RedirectToAction("HomePage", "Home");

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(vm);
            }
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("HomePage", "Home");
            }
            var vm = new UserRegistrationViewModel();
            vm.Roles = _authService.GetRoles();
            return View(vm);
        }
        [HttpPost("Register")]
        public IActionResult Register(UserRegistrationViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _authService.Register(vm);
                    return RedirectToAction("Login");
                }
                else
                {
                    throw new ValidationException("Terjadi kesalahan dalam pendaftaran akun");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                vm.Roles = _authService.GetRoles();
                return View(vm);
            }
        }
        [HttpGet("ChangePassword")]
        public IActionResult ChangePassword()
        {
            var user = HttpContext.User as ClaimsPrincipal;
            var username = user.FindFirst("username").Value;
            var vm = new UserChangePasswordViewModel();
            vm.Username = username;
            return View(vm);
        }
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(UserChangePasswordViewModel vm)
        {
            var user = HttpContext.User as ClaimsPrincipal;
            var username = user.FindFirst("username").Value;
            vm.Username = username;

            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            try
            {
                _authService.ChangePassword(vm);
                return RedirectToAction("HomePage", "Home");
            }catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(vm);
            }
        }
        [HttpGet("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
