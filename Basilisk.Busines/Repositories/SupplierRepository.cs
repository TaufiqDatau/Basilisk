using Basilisk.Busines.Interface;
using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Busines.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly BasiliskTfContext _dbContext;

        public SupplierRepository(BasiliskTfContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Supplier> GetSuppliers(string? name) {
            var result = from sup in _dbContext.Suppliers
                         where (sup.DeleteDate == null) && ( sup.CompanyName.ToLower().Contains(name.ToLower()) || String.IsNullOrEmpty(name))
                         select sup;
            return result.ToList(); 
        }

        public Supplier GetSuppliersById(long Id)
        {
            return _dbContext.Suppliers.FirstOrDefault(s => s.Id == Id && s.DeleteDate == null)
                ?? throw new KeyNotFoundException("No Supplier Foung by ID : "+ Id);
        }

        public Supplier UpdateSupplier(Supplier supplier)
        {
            if(supplier.DeleteDate != null)
            {
                throw new ArgumentException("This supplier had been deleted");
            }
            _dbContext.Suppliers.Update(supplier);
            _dbContext.SaveChanges();
            return supplier;
        }

        public Supplier InsertSupplier(Supplier supplier)
        {
            _dbContext.Add(supplier);
            _dbContext.SaveChanges();
            return supplier;
        }

        public Supplier DeleteSupplier(Supplier supplier)
        {
            if(supplier.DeleteDate != null)
            {
                throw new ArgumentException("The you trying to delete didn't exist");
            }
            supplier.DeleteDate = DateTime.Now;
            _dbContext.Update(supplier);
            _dbContext.SaveChanges();

            return supplier;
        }

        public PaginationResponse<Supplier> GetAllSupplierPage(string name, int currentPage, int pageSize)
        {
            var query = (from supplier in _dbContext.Suppliers
                        where supplier.DeleteDate == null && (supplier.CompanyName.ToLower().Contains(name.ToLower()))
                        select supplier).ToList();
            var totalItems = query.Count();

            return new PaginationResponse<Supplier>
            {
                Data = query.Skip((currentPage - 1)*pageSize).Take(pageSize).ToList(),
                Name = name,
                PageSize = pageSize,
                CurrentPage = currentPage,
                TotalItem = totalItems
            };
        }
    }
}
