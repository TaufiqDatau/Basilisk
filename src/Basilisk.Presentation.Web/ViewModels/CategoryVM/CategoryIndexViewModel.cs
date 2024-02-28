namespace Basilisk.Presentation.Web.ViewModels.CategoryVM
{
    public class CategoryIndexViewModel
    {
        public List<CategoryViewModel> Categories { get; set; }
        public string? Name { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}
