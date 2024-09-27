using FluentValidation;
using Global.Dto;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Service_API.Contract.Request.Create;
using Service_API.Contract.Request.Update;
using Service_API.Domain.Abstract.IService;
using Service_API.Domain.Model;
using Service_API.Infrastructure.Validator;

namespace Service_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationService _specializationService;
        private readonly IValidator<Specialization> _specializationValidator;
        private readonly IBus _bus;
        private readonly ILogger<SpecializationController> _logger;

        public SpecializationController(ISpecializationService specializationService, IBus bus, IValidator<Specialization> specializationValidator, ILogger<SpecializationController> logger)
        {
            _specializationService = specializationService;
            _bus = bus;
            _specializationValidator = specializationValidator;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Specialization>>> GetAll()
        {
            var result = await _specializationService.GetAllSpecialization();
            if (result.IsFailure)
            {
                _logger.LogError("Failed to retrieve all specializations: {Error}", result.Error);
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Successfully retrieved all specializations");
            return Ok(result.Value);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Specialization>> GetById(Guid id)
        {
            var result = await _specializationService.GetByIdSpecialization(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Specialization with ID {Id} not found: {Error}", id, result.Error);
                return NotFound(result.Error);
            }

            _logger.LogInformation("Successfully retrieved specialization with ID {Id}", id);
            return Ok(result.Value);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Specialization>> Create(CreateSpecializationRequest request)
        {
            var specialization = new Specialization
            {
                Id = Guid.NewGuid(),
                SpecializationName = request.SpecializationName,
                IsActive = request.IsActive
            };

            var validationResult = await _specializationValidator.ValidateAsync(specialization);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for specialization creation: {Errors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var result = await _specializationService.CreateSpecialization(specialization);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create specialization: {Error}", result.Error);
                return BadRequest(result.Error);
            }

            await PublishSpecialization(new CreateSpecialization
            {
                Specialization = new SpecializationDto
                {
                    Id = result.Value.Id,
                    Name = result.Value.SpecializationName,
                    IsActive = result.Value.IsActive
                }
            });

            _logger.LogInformation("Successfully created specialization: {Specialization}", result.Value);
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Specialization>> Update(UpdateSpecializationRequest request)
        {
            var specialization = new Specialization
            {
                Id = request.Id,
                SpecializationName = request.SpecializationName,
                IsActive = request.IsActive
            };

            var validationResult = await _specializationValidator.ValidateAsync(specialization);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for specialization update: {Errors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var result = await _specializationService.UpdateSpecialization(specialization);
            if (result.IsFailure)
            {
                _logger.LogWarning("Failed to update specialization with ID {Id}: {Error}", request.Id, result.Error);
                return NotFound(result.Error);
            }

            await PublishSpecialization(new UpdateSpecialization
            {
                Specialization = new SpecializationDto
                {
                    Id = result.Value.Id,
                    Name = result.Value.SpecializationName,
                    IsActive = result.Value.IsActive
                }
            });

            _logger.LogInformation("Successfully updated specialization: {Specialization}", result.Value);
            return Ok(result.Value);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _specializationService.DeleteSpecialization(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Failed to delete specialization with ID {Id}: {Error}", id, result.Error);
                return NotFound(result.Error);
            }

            await PublishSpecialization(new DeleteSpecialization { Id = id });
            _logger.LogInformation("Successfully deleted specialization with ID {Id}", id);
            return NoContent();
        }

        private async Task PublishSpecialization<T>(T specializationEvent)
        {
            await _bus.Publish(specializationEvent);
        }
    }

}
