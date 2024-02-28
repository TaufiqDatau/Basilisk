using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Presentation.Web.Controllers
{
    [Authorize(Roles ="Administrator, Finance")]
    [Route("[controller]")]
    public class SupplierController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
