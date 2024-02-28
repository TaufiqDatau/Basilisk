using Basilisk.Busines.Interface;
using Basilisk.Presentation.Web.ViewModels.SupplierVM;

namespace Basilisk.Presentation.Web.Services
{
    public class SupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public List<SupllierViewModel> GetAllSuppliers()
        {
            var query = _supplierRepository.GetSuppliers("");
            var result = query.Select(sup => new SupllierViewModel
            {
                Id = sup.Id,
                CompanyName = sup.CompanyName,
            });
            return result.ToList();
        }
    }
}
