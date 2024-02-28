using Basilisk.Presentation.Web.Validation;

namespace Basilisk.Presentation.Web.ViewModels.CategoryVM
{
    public class CategoryViewModel
    {
        public long Id { get; set; }
        [UniqueCategoryName]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? TotalProduct { get; set; }
    }
}
