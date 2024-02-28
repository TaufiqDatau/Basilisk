using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using Basilisk.Busines.Interface;
using Basilisk.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Basilisk.Busines.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private  readonly BasiliskTfContext _dbContext ;
        public CategoryRepository(BasiliskTfContext dBcontext)
        {
            _dbContext = dBcontext;
        }
        public List<Supplier> GetSuppliers()
        {
            var query = from supplier in _dbContext.Suppliers
                        select supplier;
            return query.ToList();
        }
        public List<Product> GetProductFromCategory(Category category)
        {
            var query = from product in _dbContext.Products
                        where product.CategoryId == category.Id 
                        select product;
            
            return query.ToList();
        }
        public List<Category> GetAllCategory()
        {
            var query = from cat in _dbContext.Categories
                        select cat;
            return query.ToList();
        } 
        public List<Category> GetAllCategory(string? name, int pageNumber, int pageSize)
        { 
            var query = from category in _dbContext.Categories
                        where name == null || category.Name.ToLower().Contains(name.ToLower())
                        select category;
            return query.ToList();
        }

        public Category GetCategory(long id)
        {
            return _dbContext.Categories.FirstOrDefault(cat => cat.Id == id)!;
        }

        public void Insert(Category obj)
        {
            _dbContext.Categories.Add(obj);
            _dbContext.SaveChanges();
        }

        public void Update(Category obj)
        {

            _dbContext.Update(obj);
            _dbContext.SaveChanges();
        }


        public void Delete(Category obj)
        {
            _dbContext.Categories.Remove(obj); 
            _dbContext.SaveChanges();  
        }

    }
}
