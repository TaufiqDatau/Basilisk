using Basilisk.Busines.Repositories;
using Basilisk.Busines.Interface;
using Basilisk.Busines;
using System.Net;
using System.Numerics;
using Basilisk.DataAccess.Models;

namespace Basilisk.Presentation.API.Suppliers
{
    public class SupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public List<SupplierDto> GetAllSuppliers(string? name)
        {
            var query = _supplierRepository.GetSuppliers(name);
            var result = query.Select(sup => new SupplierDto
            {
                Id = sup.Id,
                CompanyName = sup.CompanyName,
                ContactPerson = sup.ContactPerson,
                JobTitle = sup.JobTitle,
                Address = sup.Address,
                City = sup.City,
                Phone = sup.Phone,
                Email = sup.Email
            });
            return result.ToList();
        }
        public SupplierDto GetById(long Id)
        {
            var supplier = _supplierRepository.GetSuppliersById(Id);
            return new SupplierDto
            {
                Id = supplier.Id,
                CompanyName = supplier.CompanyName,
                ContactPerson = supplier.ContactPerson,
                JobTitle = supplier.JobTitle,
                Address = supplier.Address,
                City = supplier.City,
                Phone = supplier.Phone,
                Email = supplier.Email
            };
        }

        public SupplierDto UpdateById(SupplierDto updatedSupplier)
        {
            var supplier = _supplierRepository.GetSuppliersById(updatedSupplier.Id);

            supplier.CompanyName = updatedSupplier.CompanyName;
            supplier.Address= updatedSupplier.Address;
            supplier.Email= updatedSupplier.Email;
            supplier.City= updatedSupplier.City;
            supplier.ContactPerson= updatedSupplier.ContactPerson;
            supplier.Phone= updatedSupplier.Phone;
            supplier.JobTitle = updatedSupplier.JobTitle;

            var returnVal = _supplierRepository.UpdateSupplier(supplier);

            return new SupplierDto
            {
                Id = returnVal.Id,
                CompanyName = returnVal.CompanyName,
                ContactPerson = returnVal.ContactPerson,
                JobTitle = returnVal.JobTitle,
                Address = returnVal.Address,
                City = returnVal.City,
                Phone = returnVal.Phone,
                Email = returnVal.Email

            };
        }

        public SupplierDto InsertSupplier(SupplierDto supplier)
        {
            Supplier newSupplier = new Supplier
            {
                CompanyName = supplier.CompanyName,
                Address = supplier.Address,
                City = supplier.City,
                Phone = supplier.Phone,
                Email = supplier.Email,
                ContactPerson = supplier.ContactPerson,
                JobTitle = supplier.JobTitle,
            };

           var insertedSupplier =  _supplierRepository.InsertSupplier(newSupplier);
            return new SupplierDto
            {
                Id = insertedSupplier.Id,
                CompanyName = insertedSupplier.CompanyName,
                ContactPerson = insertedSupplier.ContactPerson,
                JobTitle = insertedSupplier.JobTitle,
                Address = insertedSupplier.Address,
                City = insertedSupplier.City,
                Phone = insertedSupplier.Phone,
                Email = insertedSupplier.Email
            };
        }

        public SupplierDto DeleteSupplier(long Id)
        {
            Supplier target = _supplierRepository.GetSuppliersById(Id);
            var DeletedSupplier = _supplierRepository.DeleteSupplier(target);
            return new SupplierDto
            {
                Id = DeletedSupplier.Id,
                CompanyName = DeletedSupplier.CompanyName,
                ContactPerson = DeletedSupplier.ContactPerson,
                JobTitle = DeletedSupplier.JobTitle,
                Address = DeletedSupplier.Address,
                City = DeletedSupplier.City,
                Phone = DeletedSupplier.Phone,
                Email = DeletedSupplier.Email
            };
        }

        public PaginationResponse<SupplierDto> GetSupplierByPage(string name, int currentPage, int pageSize)
        {
            var PageInformation = _supplierRepository.GetAllSupplierPage(name, currentPage, pageSize);

            var SupplierDtoList = PageInformation.Data.Select(sup => new SupplierDto
            {
                Id = sup.Id,
                CompanyName = sup.CompanyName,
                ContactPerson = sup.ContactPerson,
                JobTitle = sup.JobTitle,
                Address = sup.Address,
                City = sup.City,
                Phone = sup.Phone,
                Email = sup.Email
            });
            return new PaginationResponse<SupplierDto>
            {
                Data = SupplierDtoList.ToList(),
                Name = PageInformation.Name,
                CurrentPage = PageInformation.CurrentPage,
                TotalItem = PageInformation.TotalItem,
                PageSize = PageInformation.PageSize
            };
        }
    }
}
