using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Busines.Interface
{
    public interface ISupplierRepository
    {
        public List<Supplier> GetSuppliers(string? name);
        public Supplier GetSuppliersById(long Id);
        public Supplier UpdateSupplier(Supplier supplier);
        public Supplier InsertSupplier(Supplier supplier);
        public Supplier DeleteSupplier(Supplier supplier);
        public PaginationResponse<Supplier> GetAllSupplierPage(string name, int currentPage, int pageSize);
    }
}
