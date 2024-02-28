namespace Basilisk.Presentation.Web.ViewModels
{
    public class PaginationInfoViewModel
    {

        private int _totalItems;
        private int _pageSize;
        private int _currentPage;

        public int TotalItems
        {
            get { return _totalItems; }
            set
            {
                _totalItems = value;
                EnsureValidCurrentPage();
            }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
                EnsureValidCurrentPage();
            }
        }

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                EnsureValidCurrentPage();
            }
        }

        public int TotalPage
        {
            get
            {
                return (int)Math.Ceiling((double)TotalItems / PageSize);
            }
        }

        private void EnsureValidCurrentPage()
        {
            // Ensure that CurrentPage is within valid range
            if (CurrentPage < 1)
            {
                CurrentPage = 1;
            }
            else if (CurrentPage > TotalPage && TotalPage > 0)
            {
                CurrentPage = TotalPage;
            }
        }


    }
}
