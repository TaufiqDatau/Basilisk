using Basilisk.Presentation.Web.ViewModels.SupplierVM;
using Basilisk.Presentation.Web.ViewModels.CategoryVM;
using System.ComponentModel.DataAnnotations;
using Basilisk.Presentation.Web.Validation;

namespace Basilisk.Presentation.Web.ViewModels.ProductVM
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Supplier")]
        public long? SupplierId { get; set; }
        [Display(Name = "Category")]
        [Required(ErrorMessage ="Please select a Category")]
        public long? CategoryId { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        [Reorderable]
        public int OnOrder { get; set; }
        public bool Discontinue { get; set; }
        public string? SupplierName { get; set; }
        public string? CategoryName { get; set; }
        public List<CategoryViewModel>? Categories { get; set; }
        public List<SupllierViewModel>? Suppliers { get; set; }
    }
}
