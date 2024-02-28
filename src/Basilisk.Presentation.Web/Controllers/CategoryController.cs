using Microsoft.AspNetCore.Mvc;
using Basilisk.DataAccess.Models;
using Basilisk.Busines.Repositories;
using Basilisk.Presentation.Web.Services;
using Basilisk.Presentation.Web.ViewModels.CategoryVM;
using Microsoft.AspNetCore.Authorization;

namespace Basilisk.Presentation.Web.Controllers
{
    [Authorize(Roles ="Administrator")]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly CategoryService _service;

        public CategoryController(ILogger<CategoryController> logger, CategoryService service)
        {
            _logger = logger; //sudah memasukan dependency injection
            _service = service;
        }
        [HttpGet("Index")]
        public IActionResult Index(string? name = "", int page = 1)
        {

            var vm = _service.GetAll(name, page, CONSTANT.PageSize);
            return View(vm); //Jika ingin menambahkan view Add View
        }

        [HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            // Your delete logic here
            _service.DeleteCategory(id);
            return RedirectToAction("Index"); // or any other appropriate action
        }

        [HttpGet("Insert")]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost("Insert")]
        public IActionResult Insert(CategoryViewModel newCategory)
        {
            if (ModelState.IsValid)
            {
                _service.InsertNewCategory(newCategory);

                // Redirect to the "front-page" action in the same controller
                return RedirectToAction("Index");
            }

            return View(newCategory);
        }


        [HttpGet("Edit")]
        public IActionResult Update(int id)
        {
            CategoryViewModel vm = _service.GetCategoryById(id);
            return View(vm);
        }

        [HttpPost("Edit")]
        public IActionResult Update(CategoryViewModel obj)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateCategory(obj);
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        [HttpGet("products")]
        public IActionResult Detail(long Id, int pageNumber = 1)
        {
            var vm = _service.GetProductFromCategory(Id, pageNumber, CONSTANT.PageSize);
            return View(vm);
        }

        [HttpGet("deletefailed")]
        public IActionResult DeleteFail(int id)
        {
            var vm = _service.GetCategoryById(id);
            if (vm.TotalProduct > 0)
            {
                return View(vm);
            }
            return RedirectToAction("Delete", new { id = id });
        }
    }
}
