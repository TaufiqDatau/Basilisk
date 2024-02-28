using Basilisk.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Busines.Interface
{
    public interface ICategoryRepository
    {
        public List<Supplier> GetSuppliers();
        public List<Product> GetProductFromCategory(Category category);
        public List<Category> GetAllCategory();
        public List<Category> GetAllCategory(string? name, int pageNumber, int pageSize);
        public Category GetCategory(long id);
        public void Insert(Category obj);
        public void Update(Category obj);
        public void Delete(Category obj);
    }
}
