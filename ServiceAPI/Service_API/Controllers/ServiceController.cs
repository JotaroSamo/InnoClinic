using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service_API.Contract.Request.Create;
using Service_API.Contract.Request.Update;
using Service_API.Domain.Abstract.IService;
using Service_API.Domain.Model;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Service_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        private readonly IValidator<Service> _serviceValidator;
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(IServiceService serviceService, IValidator<Service> serviceValidator, ILogger<ServiceController> logger)
        {
            _serviceService = serviceService;
            _serviceValidator = serviceValidator;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Service>>> GetAll()
        {
            var result = await _serviceService.GetAllService();
            if (result.IsFailure)
            {
                _logger.LogError("Failed to get all services: {Error}", result.Error);
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Successfully retrieved all services: {Services}", JsonConvert.SerializeObject(result.Value));
            return Ok(result.Value);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Service>> GetById(Guid id)
        {
            var result = await _serviceService.GetByIdService(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Service with ID {Id} not found: {Error}", id, result.Error);
                return NotFound(result.Error);
            }

            _logger.LogInformation("Successfully retrieved service with ID {Id}: {Service}", id, JsonConvert.SerializeObject(result.Value));
            return Ok(result.Value);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Service>> Create(CreateServiceRequest request)
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

            var validationResult = await _serviceValidator.ValidateAsync(service);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for service creation: {Errors}", JsonConvert.SerializeObject(validationResult.Errors));
                return BadRequest(validationResult.Errors);
            }

            var result = await _serviceService.CreateService(service);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create service: {Error}", result.Error);
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Successfully created service: {Service}", JsonConvert.SerializeObject(result.Value));
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Service>> Update(UpdateServiceRequest request)
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

            var validationResult = await _serviceValidator.ValidateAsync(service);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for service update: {Errors}", JsonConvert.SerializeObject(validationResult.Errors));
                return BadRequest(validationResult.Errors);
            }

            var result = await _serviceService.UpdateService(service);
            if (result.IsFailure)
            {
                _logger.LogWarning("Failed to update service with ID {Id}: {Error}", request.Id, result.Error);
                return NotFound(result.Error);
            }

            _logger.LogInformation("Successfully updated service: {Service}", JsonConvert.SerializeObject(result.Value));
            return Ok(result.Value);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _serviceService.Delete(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Failed to delete service with ID {Id}: {Error}", id, result.Error);
                return NotFound(result.Error);
            }

            _logger.LogInformation("Successfully deleted service with ID {Id}", id);
            return NoContent();
        }

        [HttpPatch("ChangeStatus/{id}")]
        public async Task<ActionResult> ChangeStatus(Guid id, [FromQuery] bool status)
        {
            var result = await _serviceService.ChangeStatusService(id, status);
            if (result.IsFailure)
            {
                _logger.LogWarning("Failed to change status for service with ID {Id}: {Error}", id, result.Error);
                return NotFound(result.Error);
            }

            _logger.LogInformation("Successfully changed status for service with ID {Id}: {Status}", id, status);
            return Ok(result.Value);
        }
    }


}
