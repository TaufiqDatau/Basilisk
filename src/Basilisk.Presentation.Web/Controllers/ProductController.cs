using Microsoft.AspNetCore.Mvc;
using Basilisk.Busines.Interface;
using Basilisk.Presentation.Web.Services;
using Basilisk.Presentation.Web.ViewModels.ProductVM;
using Microsoft.AspNetCore.Authorization;

namespace Basilisk.Presentation.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly SupplierService _supplierService;

        public ProductController(ProductService productService, CategoryService categoryService, SupplierService supplierService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
        }

        [HttpGet("Index")]
        public IActionResult Index(string name = "", int supplierid = 0, int categoryid = 0, int pagenumber = 1)
        {
            var vm = _productService.GetAllProducts(name, supplierid, categoryid, pagenumber, CONSTANT.PageSize);
            vm.Categories = _categoryService.GetCategoryViewModels();
            vm.Suppliers = _supplierService.GetAllSuppliers();
            return View(vm);
        }
        [Authorize(Roles ="Administrator")]
        [HttpGet("Insert")]
        public IActionResult Insert()
        {
            var vm = new ProductViewModel();
            vm.Categories = _categoryService.GetCategoryViewModels();
            vm.Suppliers = _supplierService.GetAllSuppliers();
            return View(vm);
        }

        [HttpPost("Insert")]
        public IActionResult Insert(ProductViewModel pro)
        {
            if(ModelState.IsValid)
            {
                _productService.InsertNewProduct(pro);
                return RedirectToAction("Index");
            }
            pro.Suppliers= _supplierService.GetAllSuppliers();
            pro.Categories= _categoryService.GetCategoryViewModels();
            return View("Insert",pro);
        }

        [Authorize(Roles ="Administrator")]
        [HttpGet("Edit")]
        public IActionResult Edit(long id)
        {
            ProductViewModel vm = _productService.GetProductById(id);
            vm.Categories=_categoryService.GetCategoryViewModels();
            vm.Suppliers = _supplierService.GetAllSuppliers();
            return View(vm);
        }

        [HttpPost("Edit")]
        public IActionResult Edit(ProductViewModel vm)
        {
            if(ModelState.IsValid)
            {
                _productService.UpdateProduct(vm);
                return RedirectToAction("Index");
            }
            vm.Categories = _categoryService.GetCategoryViewModels();
            vm.Suppliers = _supplierService.GetAllSuppliers();
            return View(vm);
        }
        [Authorize(Roles ="Administrator")]
        [HttpGet("Delete")]
        public IActionResult Delete(long id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        [HttpGet("Detail/{Id}")]
        public IActionResult Detail(long Id) {
            var vm = _productService.GetProductDetail(Id);
            return View(vm);
        }
    }
}
