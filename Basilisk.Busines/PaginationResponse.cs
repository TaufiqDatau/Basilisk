using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Busines
{
    public class PaginationResponse<T>
    {
        public List<T> Data { get; set; }
        public string Name { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItem { get; set; }
        public int TotalPage
        {
            get
            {
                return (int) (Math.Ceiling((Decimal)TotalItem / PageSize));
            }
        }
    }
}
