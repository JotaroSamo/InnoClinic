using FluentValidation;
using Global.Dto;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Profile_API.Application.Service;
using Profile_API.Contract.Request.Create;
using Profile_API.Contract.Request.Update;
using Profile_API.Domain.Models;

namespace Profile_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IValidator<Patient> _patientValidator; // Добавьте валидатор для валидации сущности Patient
        private readonly ILogger<PatientController> _logger; // Добавьте логгер для логирования действий
        private readonly IBus _bus;

        public PatientController(IPatientService patientService, IValidator<Patient> patientValidator, ILogger<PatientController> logger, IBus bus)
        {
            _patientService = patientService;
            _patientValidator = patientValidator;
            _logger = logger;
            _bus = bus;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Patient>> Create([FromBody] CreatePatientRequest createPatientRequest)
        {
            if (createPatientRequest == null)
            {
                _logger.LogWarning("CreatePatientRequest is null.");
                return BadRequest("Patient data is required.");
            }

            var patient = new Patient
            {
                FirstName = createPatientRequest.FirstName,
                LastName = createPatientRequest.LastName,
                MiddleName = createPatientRequest.MiddleName,
                IsLinkedToAccount = createPatientRequest.IsLinkedToAccount,
                DateOfBirth = createPatientRequest.DateOfBirth,
                AccountId = createPatientRequest.AccountId
            };

            // Валидация сущности Patient
            var validationResult = await _patientValidator.ValidateAsync(patient);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for patient creation: {Errors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var result = await _patientService.CreatePatientAsync(patient);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create patient: {Error}", result.Error);
                return BadRequest(result.Error);
            }
            var busPatient = await _patientService.GetPatientByIdAsync(result.Value.Id);
            var publish = new CreatePatient
            {
                Patient = new PatientAppointmentDto
                {
                    Id= busPatient.Value.Id,
                    Patient_Name = busPatient.Value.FirstName + " " + busPatient.Value.LastName + " " + busPatient.Value.MiddleName,
                    Number_Phone = busPatient.Value.Account.PhoneNumber
                    
                }
            };
            await _bus.Publish(publish);
            _logger.LogInformation("Patient created successfully: {PatientId}", result.Value.Id);
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Patient>> GetById(Guid id)
        {
            var result = await _patientService.GetPatientByIdAsync(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Patient with ID {Id} not found: {Error}", id, result.Error);
                return NotFound(result.Error);
            }

            _logger.LogInformation("Retrieved patient with ID {Id}", id);
            return Ok(result.Value);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Patient>>> GetAll()
        {
            var result = await _patientService.GetAllPatientsAsync();
            if (result.IsFailure)
            {
                _logger.LogError("Failed to retrieve patients: {Error}", result.Error);
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Retrieved all patients");
            return Ok(result.Value);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Patient>> Update([FromBody] UpdatePatientRequest updatePatientRequest)
        {
            if (updatePatientRequest == null)
            {
                _logger.LogWarning("UpdatePatientRequest is null.");
                return BadRequest("Patient data is required.");
            }

            var patient = new Patient
            {
                Id = updatePatientRequest.Id,
                FirstName = updatePatientRequest.FirstName,
                LastName = updatePatientRequest.LastName,
                MiddleName = updatePatientRequest.MiddleName,
                IsLinkedToAccount = updatePatientRequest.IsLinkedToAccount,
                DateOfBirth = updatePatientRequest.DateOfBirth,
                AccountId = updatePatientRequest.AccountId
            };

            // Валидация сущности Patient
            var validationResult = await _patientValidator.ValidateAsync(patient);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for patient update: {Errors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var result = await _patientService.UpdatePatientAsync(patient.Id, patient);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to update patient: {Error}", result.Error);
                return BadRequest(result.Error);
            }
            var busPatient = await _patientService.GetPatientByIdAsync(result.Value.Id);
            var publish = new UpdatePatient
            {
                Patient = new PatientAppointmentDto
                {
                    Id = busPatient.Value.Id,
                    Patient_Name = busPatient.Value.FirstName + " " + busPatient.Value.LastName + " " + busPatient.Value.MiddleName,
                    Number_Phone = busPatient.Value.Account.PhoneNumber

                }
            };
            await _bus.Publish(publish);
            _logger.LogInformation("Patient updated successfully: {PatientId}", result.Value.Id);
            return Ok(result.Value);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _patientService.DeletePatientAsync(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Failed to delete patient with ID {Id}: {Error}", id, result.Error);
                return NotFound(result.Error);
            }
           
            var publish = new DeletePatient
            {
                Id = id
            };
            await _bus.Publish(publish);
            _logger.LogInformation("Patient with ID {Id} deleted successfully", id);
            return NoContent();
        }
    }

}
