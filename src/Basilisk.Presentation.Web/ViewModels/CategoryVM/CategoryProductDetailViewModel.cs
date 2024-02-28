using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.ViewModels.ProductVM;

namespace Basilisk.Presentation.Web.ViewModels.CategoryVM
{
    public class CategoryProductDetailViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public Category Category { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}
