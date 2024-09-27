using Microsoft.AspNetCore.Mvc;
using Service_API.Contract.Request.Create;
using Service_API.Contract.Request.Update;
using Service_API.Domain.Abstract.IService;
using Service_API.Domain.Model;

namespace Service_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceCategoryController : ControllerBase
    {
        private readonly IServiceCategoryService _serviceCategoryService;

        public ServiceCategoryController(IServiceCategoryService serviceCategoryService)
        {
            _serviceCategoryService = serviceCategoryService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _serviceCategoryService.GetAllCategoryService();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _serviceCategoryService.GetByIdCategoryService(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateServiceCategoryRequest request)
        {
            var serviceCategory = new ServiceCategory
            {
                Id = Guid.NewGuid(), 
                CategoryName = request.CategoryName,
                TimeSlotSize = request.TimeSlotSize
            };

            if (!TryValidateModel(serviceCategory))
            {
                return BadRequest();
            }

            var result = await _serviceCategoryService.CreateCategoryService(serviceCategory);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateServiceCategoryRequest request)
        {
            var serviceCategory = new ServiceCategory
            {
                Id = request.Id,
                CategoryName = request.CategoryName,
                TimeSlotSize = request.TimeSlotSize
            };

            if (!TryValidateModel(serviceCategory))
            {
                return BadRequest();
            }

            var result = await _serviceCategoryService.UpdateCategoryService(serviceCategory);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _serviceCategoryService.DeleteCategoryService(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return NoContent();
        }
    }
}
