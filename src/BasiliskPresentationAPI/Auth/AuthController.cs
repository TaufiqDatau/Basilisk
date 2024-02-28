using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Presentation.API.Auth
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authServices;
        public AuthController(AuthService authServices)
        {
            _authServices = authServices;
        }
        [HttpPost]
        public IActionResult Login(AuthRequestDTO request)
        {
            var response = _authServices.CreateToken(request);
            return Ok(response);
        }
    }
}
