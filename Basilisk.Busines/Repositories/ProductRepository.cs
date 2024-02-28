using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basilisk.Busines.Interface;
using Basilisk.DataAccess.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Basilisk.Busines.Repositories
{
    public class ModelTest
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public long CategoryId { get; set; }

    }
    public class ProductRepository: IProductRepository
    {
        private readonly BasiliskTfContext _dbContext;
        public ProductRepository(BasiliskTfContext dBcontext)
        {
            _dbContext = dBcontext;
        }
        public List<Product> GetAllProductsByCategoryId(long Id)
        {
            var query = from product in _dbContext.Products
                        where product.CategoryId == Id
                        select product;
            return query.ToList();
        }
        public List<Product> GetAllProducts(string name, int supplierId, int categoryId)
        {
            //var procedureResult = _dbContext.Products.FromSqlInterpolated($"EXEC [dbo].[SearchProductByName] {name}").ToList(); menggunakan procedure dalam .NET CORE
            var result = from product in _dbContext.Products
                         .Include(product=>product.Category)
                         .Include(product=>product.Supplier)
                         where (name==null || product.Name.ToLower().Contains(name.ToLower()))
                         && (supplierId==0 || product.SupplierId==supplierId)
                         && (categoryId==0 || product.CategoryId==categoryId)
                         select product;
            return result.ToList();
        }

        public void InsertNewProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public Product GetProductById(long Id)
        {
            return _dbContext.Products.Include(p=>p.Supplier).Include(p=>p.Category).FirstOrDefault(p => p.Id == Id);
        }

        public void DeleteProduct(Product pro)
        {
            _dbContext.Products.Remove(pro);
            _dbContext.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {
            _dbContext.Update(product);
            _dbContext.SaveChanges();
        }

        public List<Order> GetOrdersByProductId(long Id)
        {
            var query = from product in _dbContext.Products
                        .Include(p => p.OrderDetails)
                        .ThenInclude(od => od.InvoiceNumberNavigation)
                        .Where(p => p.Id == Id)
                        from orderDetail in product.OrderDetails
                        orderby orderDetail.InvoiceNumberNavigation.OrderDate
                        select orderDetail.InvoiceNumberNavigation;
            return query.ToList();
        }

        public List<OrderDetail> GetOrdersDetailByProductId(long Id)
        {
            var query = from orderDetail in _dbContext.OrderDetails
                        .Include(od=> od.InvoiceNumberNavigation)
                        .Where(od => od.ProductId == Id)
                        orderby orderDetail.InvoiceNumberNavigation.OrderDate
                        select orderDetail;
            return query.ToList();
        }

    }
}
