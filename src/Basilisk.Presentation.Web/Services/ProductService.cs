using Basilisk.DataAccess.Models;
using Basilisk.Busines.Interface;
using Basilisk.Presentation.Web.ViewModels.ProductVM;
using Basilisk.Presentation.Web.ViewModels;

namespace Basilisk.Presentation.Web.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public ProductIndexViewModel GetAllProducts(string name, int supplierId, int categoryId, int pageNumber, int pageSize)
        {
            var query = _productRepository.GetAllProducts(name, supplierId, categoryId);
            int totalItem = query.Count();
            int totalPage = (int)Math.Ceiling((decimal)totalItem / 10);

            var ProductList = query.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                SupplierId = p.SupplierId,
                CategoryId = p.CategoryId,
                Stock = p.Stock,
                OnOrder = p.OnOrder,
                SupplierName = p.Supplier?.CompanyName,
                CategoryName = p.Category.Name,
                Discontinue = p.Discontinue,
                Price = p.Price
            });

            return new ProductIndexViewModel
            {
                Products = ProductList.Skip((pageNumber - 1) * 10).Take(10).ToList(),
                Name = name,
                SupplierId = supplierId,
                CategoryId = categoryId,
                PaginationInfo = new PaginationInfoViewModel
                {
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalItems = totalItem,
                }

            };
        }

        public void InsertNewProduct(ProductViewModel product)
        {
            Product newProduct = new Product
            {
                Name = product.Name,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId.Value,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                OnOrder = product.OnOrder,
                Discontinue = product.Discontinue
            };
            _productRepository.InsertNewProduct(newProduct);

        }

        public ProductViewModel GetProductById(long id)
        {
            Product pro = _productRepository.GetProductById(id);
            ProductViewModel result = new ProductViewModel
            {
                Id = pro.Id,
                Name = pro.Name,
                Description = pro.Description,
                CategoryId = pro.CategoryId,
                SupplierId = pro.SupplierId,
                Price = pro.Price,
                Stock = pro.Stock,
                OnOrder = pro.OnOrder,
                Discontinue = pro.Discontinue
            };
            return result;
        }
        public void DeleteProduct(long Id)
        {
            var productToDelete = _productRepository.GetProductById(Id);
            _productRepository.DeleteProduct(productToDelete);
        }
        public void UpdateProduct(ProductViewModel product)
        {
            Product EditedProduct = _productRepository.GetProductById(product.Id);
            EditedProduct.Name = product.Name;
            EditedProduct.Description = product.Description;
            EditedProduct.Price = product.Price;
            EditedProduct.Stock = product.Stock;
            EditedProduct.OnOrder = product.OnOrder;
            EditedProduct.Discontinue = product.Discontinue;
            EditedProduct.CategoryId = product.CategoryId.Value;
            EditedProduct.SupplierId = product.SupplierId;

            _productRepository.UpdateProduct(EditedProduct);
        }

        public ProductDetailViewModel GetProductDetail(long Id)
        {
            var product = _productRepository.GetProductById(Id);
            var ordersDetails = _productRepository.GetOrdersDetailByProductId(Id);

            var monthlyReports = from orderDetail in ordersDetails
                                 group orderDetail by new
                                 {
                                     OrderYear = orderDetail.InvoiceNumberNavigation.OrderDate.Year,
                                     OrderMonth = orderDetail.InvoiceNumberNavigation.OrderDate.ToString("MMMM")
                                 } into grouped
                                 select new MonthlyReportRowViewModel
                                 {
                                     Year = grouped.Key.OrderYear,
                                     Month = grouped.Key.OrderMonth,
                                     Quantity = grouped.Sum(x => x.Quantity),
                                     NetTotal = grouped.Sum(x => x.Quantity * x.UnitPrice - x.Quantity*x.UnitPrice*(x.Discount/100))
                                 };

            var yearlyReports = from orderDetail in ordersDetails
                                group orderDetail by new
                                {
                                    OrderYear = orderDetail.InvoiceNumberNavigation.OrderDate.Year,
                                } into grouped
                                select new YearlyReportRowViewModel
                                {
                                    Year = grouped.Key.OrderYear,
                                    Sold = grouped.Sum(x => x.Quantity),
                                    NetTotal = grouped.Sum(x => x.Quantity * x.UnitPrice - x.Quantity * x.UnitPrice * (x.Discount / 100))
                                };

            var result = new ProductDetailViewModel
            {
                Product = new ProductViewModel
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    OnOrder = product.OnOrder,
                    Discontinue = product.Discontinue,
                    CategoryName=product.Category.Name,
                    SupplierName=product.Supplier?.CompanyName
                },
                MonthlyReportTable = monthlyReports.ToList(),
                YearlyReportTable = yearlyReports.ToList()
            };

            return result;
        }
    }
}
