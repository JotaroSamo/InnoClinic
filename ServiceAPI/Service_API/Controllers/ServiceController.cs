using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service_API.Contract.Request.Create;
using Service_API.Contract.Request.Update;
using Service_API.Domain.Abstract.IService;
using Service_API.Domain.Model;

namespace Service_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _serviceService.GetAllService();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _serviceService.GetByIdService(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateServiceRequest request)
        {
            var service = new Service
            {
                Id = Guid.NewGuid(),  
                CategoryId = request.CategoryId,
                ServiceName = request.ServiceName,
                Price = request.Price,
                SpecializationId = request.SpecializationId,
                IsActive = request.IsActive
            };
            if (!TryValidateModel(service))
            {
                return BadRequest();
            }
            var result = await _serviceService.CreateService(service);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateServiceRequest request)
        {

            var service = new Service
            {
                Id = request.Id,
                CategoryId = request.CategoryId,
                ServiceName = request.ServiceName,
                Price = request.Price,
                SpecializationId = request.SpecializationId,
                IsActive = request.IsActive
            };
            if (!TryValidateModel(service))
            {
                return BadRequest();
            }
            var result = await _serviceService.UpdateService(service);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _serviceService.Delete(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [HttpPatch("ChangeStatus/{id}")]
        public async Task<IActionResult> ChangeStatus(Guid id, [FromQuery] bool status)
        {
            var result = await _serviceService.ChangeStatusService(id, status);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
    }

}
