using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basilisk.DataAccess.Models;

namespace Basilisk.Busines.Interface
{
    public interface IProductRepository
    {
        public List<Product> GetAllProductsByCategoryId(long Id);
        public List<Product> GetAllProducts(string name, int supplierId, int categoryId);
        public void InsertNewProduct(Product product);
        public Product GetProductById(long Id);
        public void UpdateProduct(Product product);
        public void DeleteProduct(Product pro);
        public List<Order> GetOrdersByProductId(long Id);
        public List<OrderDetail> GetOrdersDetailByProductId(long Id);

    }
}
