using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Profile_API.Application.Service;
using Profile_API.Contract.Request.Create;
using Profile_API.Contract.Request.Update;
using Profile_API.Domain.Models;

namespace Profile_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceptionistController : ControllerBase
    {
        private readonly IReceptionistService _receptionistService;
        private readonly IValidator<Receptionist> _receptionistValidator; // Добавьте валидатор для валидации сущности Receptionist
        private readonly ILogger<ReceptionistController> _logger; // Добавьте логгер для логирования действий

        public ReceptionistController(IReceptionistService receptionistService, IValidator<Receptionist> receptionistValidator, ILogger<ReceptionistController> logger)
        {
            _receptionistService = receptionistService;
            _receptionistValidator = receptionistValidator;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Receptionist>> Create([FromBody] CreateReceptionistRequest createReceptionistRequest)
        {
            if (createReceptionistRequest == null)
            {
                _logger.LogWarning("CreateReceptionistRequest is null.");
                return BadRequest("Receptionist data is required.");
            }

            var receptionist = new Receptionist
            {
                FirstName = createReceptionistRequest.FirstName,
                LastName = createReceptionistRequest.LastName,
                MiddleName = createReceptionistRequest.MiddleName,
                OfficeAddress = createReceptionistRequest.OfficeAddress,
                OfficeRegistryPhoneNumber = createReceptionistRequest.OfficeRegistryPhoneNumber,
                AccountId = createReceptionistRequest.AccountId,
            };

            // Валидация сущности Receptionist
            var validationResult = await _receptionistValidator.ValidateAsync(receptionist);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for receptionist creation: {Errors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var result = await _receptionistService.CreateReceptionistAsync(receptionist);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create receptionist: {Error}", result.Error);
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Receptionist created successfully: {ReceptionistId}", result.Value.Id);
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Receptionist>> GetById(Guid id)
        {
            var result = await _receptionistService.GetReceptionistByIdAsync(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Receptionist with ID {Id} not found: {Error}", id, result.Error);
                return NotFound(result.Error);
            }

            _logger.LogInformation("Retrieved receptionist with ID {Id}", id);
            return Ok(result.Value);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Receptionist>>> GetAll()
        {
            var result = await _receptionistService.GetAllReceptionistsAsync();
            if (result.IsFailure)
            {
                _logger.LogError("Failed to retrieve receptionists: {Error}", result.Error);
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Retrieved all receptionists");
            return Ok(result.Value);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Receptionist>> Update([FromBody] UpdateReceptionistRequest updateReceptionistRequest)
        {
            if (updateReceptionistRequest == null)
            {
                _logger.LogWarning("UpdateReceptionistRequest is null.");
                return BadRequest("Receptionist data is required.");
            }

            var receptionist = new Receptionist
            {
                Id = updateReceptionistRequest.Id,
                FirstName = updateReceptionistRequest.FirstName,
                LastName = updateReceptionistRequest.LastName,
                MiddleName = updateReceptionistRequest.MiddleName,
                OfficeAddress = updateReceptionistRequest.OfficeAddress,
                OfficeRegistryPhoneNumber = updateReceptionistRequest.OfficeRegistryPhoneNumber,
                AccountId = updateReceptionistRequest.AccountId,
            };

            // Валидация сущности Receptionist
            var validationResult = await _receptionistValidator.ValidateAsync(receptionist);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for receptionist update: {Errors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var result = await _receptionistService.UpdateReceptionistAsync(receptionist.Id, receptionist);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to update receptionist: {Error}", result.Error);
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Receptionist updated successfully: {ReceptionistId}", result.Value.Id);
            return Ok(result.Value);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _receptionistService.DeleteReceptionistAsync(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Failed to delete receptionist with ID {Id}: {Error}", id, result.Error);
                return NotFound(result.Error);
            }

            _logger.LogInformation("Receptionist with ID {Id} deleted successfully", id);
            return NoContent();
        }
    }

}
