using Global.Dto;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Service_API.Contract.Request.Create;
using Service_API.Contract.Request.Update;
using Service_API.Domain.Abstract.IService;
using Service_API.Domain.Model;

namespace Service_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationService _specializationService;
        private readonly IBus _bus;
        public SpecializationController(ISpecializationService specializationService, IBus bus)
        {
            _specializationService = specializationService;
            _bus = bus;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _specializationService.GetAllSpecialization();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _specializationService.GetByIdSpecialization(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateSpecializationRequest request)
        {
            var specialization = new Specialization
            {
                Id = Guid.NewGuid(), 
                SpecializationName = request.SpecializationName,
                IsActive = request.IsActive
            };

            if (!TryValidateModel(specialization))
            {
                return BadRequest();
            }

            var result = await _specializationService.CreateSpecialization(specialization);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            var specializationPublish = new CreateSpecialization
            {
                Specialization = new SpecializationDto
                {
                    Id = result.Value.Id,
                    Name = result.Value.SpecializationName,
                    IsActive = result.Value.IsActive
                }
            };
            await _bus.Publish(specializationPublish);
            return Ok(result.Value);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateSpecializationRequest request)
        {
            var specialization = new Specialization
            {
                Id = request.Id,
                SpecializationName = request.SpecializationName,
                IsActive = request.IsActive
            };

            if (!TryValidateModel(specialization))
            {
                return BadRequest();
            }

            var result = await _specializationService.UpdateSpecialization(specialization);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            var specializationPublish = new UpdateSpecialization
            {
                Specialization = new SpecializationDto
                {
                    Id = result.Value.Id,
                    Name = result.Value.SpecializationName,
                    IsActive = result.Value.IsActive
                }
            };
            await _bus.Publish(specializationPublish);
            return Ok(result.Value);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _specializationService.DeleteSpecialization(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            var specializationPublish = new DeleteSpecialization
            {
                Id = id,
            };
            await _bus.Publish(specializationPublish);
            return NoContent();
        }
    }
}
