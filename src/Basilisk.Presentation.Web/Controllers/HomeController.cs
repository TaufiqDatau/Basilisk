using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Presentation.Web.Controllers
{
    public class HomeController: Controller
    {
        public IActionResult HomePage()
        {
            return View();
        }
    }
}
