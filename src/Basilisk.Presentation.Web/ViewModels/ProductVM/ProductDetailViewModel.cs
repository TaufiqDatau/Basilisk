namespace Basilisk.Presentation.Web.ViewModels.ProductVM
{
    public class ProductDetailViewModel
    {
        public ProductViewModel Product { get; set; }
        public List<MonthlyReportRowViewModel> MonthlyReportTable { get; set; }
        public List<YearlyReportRowViewModel> YearlyReportTable { get; set; }
    }
}
