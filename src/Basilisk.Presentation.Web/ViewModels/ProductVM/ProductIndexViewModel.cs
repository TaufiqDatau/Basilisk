using Basilisk.Presentation.Web.ViewModels.CategoryVM;
using Basilisk.Presentation.Web.ViewModels.SupplierVM;
using Basilisk.DataAccess.Models;

namespace Basilisk.Presentation.Web.ViewModels.ProductVM
{
    public class ProductIndexViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public string? Name { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public List<CategoryViewModel>? Categories { get; set; }
        public List<SupllierViewModel>? Suppliers { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}

