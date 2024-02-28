using Basilisk.Presentation.Web.ViewModels;
using Basilisk.Busines.Repositories;
using Microsoft.EntityFrameworkCore;
using Basilisk.DataAccess.Models;
using Basilisk.Busines.Interface;
using Basilisk.Presentation.Web.ViewModels.CategoryVM;
using Basilisk.Presentation.Web.ViewModels.ProductVM;

namespace Basilisk.Presentation.Web.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public CategoryProductDetailViewModel GetProductFromCategory(long Id, int pagenumber, int pageSize)
        {
           
            var category = _categoryRepository.GetCategory(Id);
            var query = _categoryRepository.GetProductFromCategory(category);
            var supplier = _categoryRepository.GetSuppliers();
            var ProductSupplierJoin = from product in query
                                      join sup in supplier
                                      on product.SupplierId equals sup.Id
                                      select new ProductViewModel{
                                        Id = product.Id,
                                        Name = product.Name,
                                        SupplierName = sup.CompanyName,
                                        Price = product.Price
                                      };
            
            int totalItem = query.Count();
            int totalPage = (int)Math.Ceiling((double)totalItem / 10);
            return new CategoryProductDetailViewModel
            {
                Products = ProductSupplierJoin.ToList(),
                Category = category,
                PaginationInfo = new PaginationInfoViewModel
                {
                    TotalItems = totalItem,
                    CurrentPage = pagenumber,
                    PageSize = pageSize 
                }
            };
        }
        public List<CategoryViewModel> GetCategoryViewModels()
        {
            var query= _categoryRepository.GetAllCategory();
            var result = query.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            });
            return result.ToList();

        }
        public CategoryIndexViewModel GetAll(string? _name, int pageNumber, int pageSize)
        {
            List<CategoryViewModel> result;
            int totalItem;
      
            var query = _categoryRepository.GetAllCategory(_name,pageNumber,pageSize);

            result = query
                .Select(c => new CategoryViewModel { Id = c.Id, Name = c.Name, Description = c.Description })
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .ToList();

            totalItem = query.Count();

            return new CategoryIndexViewModel
            {
                Categories = result,
                Name = _name,
                PaginationInfo = new PaginationInfoViewModel { 
                    TotalItems = totalItem,
                    CurrentPage = pageNumber,
                    PageSize = pageSize
                }
            };
        }


        public CategoryViewModel GetCategoryById(int id)
        {
            CategoryViewModel result= new CategoryViewModel();
            Category extracted = _categoryRepository.GetCategory(id);
            result.Id= extracted.Id;
            result.Name = extracted.Name;
            result.Description = extracted.Description;
            var query = _categoryRepository.GetProductFromCategory(extracted);
            result.TotalProduct = query.Count();
            return result;
        }

        public void InsertNewCategory(CategoryViewModel newCategory)
        {
            Category insertedCategory= new Category() ;
            insertedCategory.Name= newCategory.Name;
            insertedCategory.Description = newCategory.Description;
            _categoryRepository.Insert(insertedCategory);
        }

        public void DeleteCategory(int id)
        {
            Category _category = _categoryRepository.GetCategory(id);
            _categoryRepository.Delete(_category);
        }

        public void UpdateCategory(CategoryViewModel category)
        {
            Category updatedCategory = _categoryRepository.GetCategory(category.Id);
            updatedCategory.Name = category.Name;
            updatedCategory.Description = category.Description;
            
            _categoryRepository.Update(updatedCategory);
        }
    }
}
