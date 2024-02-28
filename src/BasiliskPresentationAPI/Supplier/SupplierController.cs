using Microsoft.AspNetCore.Mvc;
using Basilisk.Presentation.Web;
using Microsoft.AspNetCore.Authorization;
namespace Basilisk.Presentation.API.Suppliers
{
    [ApiController]//MEMBERI TAHU BAHWA INI ADALAH API
    [Route("api/v1/suppliers")]
    public class SupplierController : ControllerBase
    {
        private readonly SupplierService _service;

        public SupplierController(SupplierService service)
        {
            _service = service;
        }
        [Authorize(Roles ="Administrator, Finance")]
        [HttpGet]
        public IActionResult GetAll(int currentPage=1,string? name="" )
        {
            if (name == null)
            {
                name = "";
            }
            var dto = _service.GetSupplierByPage(name,currentPage, CONSTANT.PageSize);
            return Ok(dto);
        }
        [Authorize(Roles ="Administrator, Finance")]
        [HttpGet("{Id?}")]
        public IActionResult GetById(long Id)
        {
            if (Id < 0)
            {
                return BadRequest(new
                {
                    Title = "ID NYA GAADA GOBLOK",
                    Status=400
                });
            }
     
            var dto = _service.GetById(Id);
            return Ok(dto);

            
        }

        [Authorize(Roles ="Administrator")]
        [HttpPost]
        public IActionResult Insert(SupplierDto dto)
        {
            var createdSup=_service.InsertSupplier(dto);
            return Created("", createdSup);
        }
        [Authorize(Roles ="Administrator")]
        [HttpPut]
        public IActionResult Update(SupplierDto sup)
        {
  
                
                return Ok(_service.UpdateById(sup));
            
        }

        [Authorize(Roles ="Administrator")]
        [HttpDelete("{Id}")]
        public IActionResult Delete(long Id)
        {
     
                return Ok(_service.DeleteSupplier(Id));
     
        }
    }
}
