using Microsoft.AspNetCore.Mvc;

namespace Profile_API.Controllers
{
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc;
    using Profile_API.Application.Service;
    using Profile_API.Contract.Request.Create;
    using Profile_API.Contract.Request.Update;
    using Profile_API.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IValidator<Doctor> _doctorValidator;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(IDoctorService doctorService, IValidator<Doctor> doctorValidator, ILogger<DoctorController> logger)
        {
            _doctorService = doctorService;
            _doctorValidator = doctorValidator;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Doctor>> Create([FromBody] CreateDoctorRequest createDoctorRequest)
        {
            if (createDoctorRequest == null)
            {
                _logger.LogWarning("CreateDoctorRequest is null.");
                return BadRequest("Doctor data is required.");
            }

            var doctor = new Doctor
            {
                FirstName = createDoctorRequest.FirstName,
                LastName = createDoctorRequest.LastName,
                MiddleName = createDoctorRequest.MiddleName,
                DateOfBirth = createDoctorRequest.DateOfBirth,
                CareerStartYear = createDoctorRequest.CareerStartYear,
                Status = createDoctorRequest.Status,
                SpecializationId = createDoctorRequest.SpecializationId,
                OfficeAddress = createDoctorRequest.OfficeAddress,
                OfficeRegistryPhoneNumber = createDoctorRequest.OfficeRegistryPhoneNumber,
                AccountId = createDoctorRequest.AccountId,
            };

            var validationResult = await _doctorValidator.ValidateAsync(doctor);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for doctor creation: {Errors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var result = await _doctorService.CreateDoctorAsync(doctor);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create doctor: {Error}", result.Error);
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Doctor created successfully: {DoctorId}", result.Value.Id);
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Doctor>> GetById(Guid id)
        {
            var result = await _doctorService.GetDoctorByIdAsync(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Doctor with ID {Id} not found: {Error}", id, result.Error);
                return NotFound(result.Error);
            }

            _logger.LogInformation("Retrieved doctor with ID {Id}", id);
            return Ok(result.Value);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Doctor>>> GetAll()
        {
            var result = await _doctorService.GetAllDoctorsAsync();
            if (result.IsFailure)
            {
                _logger.LogError("Failed to retrieve doctors: {Error}", result.Error);
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Retrieved all doctors");
            return Ok(result.Value);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Doctor>> Update([FromBody] UpdateDoctorRequest updateDoctorRequest)
        {
            if (updateDoctorRequest == null)
            {
                _logger.LogWarning("UpdateDoctorRequest is null.");
                return BadRequest("Doctor data is required.");
            }

            var doctor = new Doctor
            {
                Id = updateDoctorRequest.Id,
                FirstName = updateDoctorRequest.FirstName,
                LastName = updateDoctorRequest.LastName,
                MiddleName = updateDoctorRequest.MiddleName,
                DateOfBirth = updateDoctorRequest.DateOfBirth,
                CareerStartYear = updateDoctorRequest.CareerStartYear,
                Status = updateDoctorRequest.Status,
                SpecializationId = updateDoctorRequest.SpecializationId,
                OfficeAddress = updateDoctorRequest.OfficeAddress,
                OfficeRegistryPhoneNumber = updateDoctorRequest.OfficeRegistryPhoneNumber,
                AccountId = updateDoctorRequest.AccountId,
            };

            var validationResult = await _doctorValidator.ValidateAsync(doctor);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for doctor update: {Errors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var result = await _doctorService.UpdateDoctorAsync(updateDoctorRequest.Id, doctor);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to update doctor: {Error}", result.Error);
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Doctor updated successfully: {DoctorId}", result.Value.Id);
            return Ok(result.Value);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _doctorService.DeleteDoctorAsync(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Failed to delete doctor with ID {Id}: {Error}", id, result.Error);
                return NotFound(result.Error);
            }

            _logger.LogInformation("Doctor with ID {Id} deleted successfully", id);
            return NoContent();
        }

        [HttpGet("GetByName")]
        public async Task<ActionResult<Doctor>> GetByName(string firstName, string lastName, string middleName)
        {
            var result = await _doctorService.GetDoctorByNameAsync(firstName, lastName, middleName);
            if (result.IsFailure)
            {
                _logger.LogWarning("Doctor not found with name {FirstName} {LastName} {MiddleName}: {Error}", firstName, lastName, middleName, result.Error);
                return NotFound(result.Error);
            }

            _logger.LogInformation("Retrieved doctor by name: {FirstName} {LastName} {MiddleName}", firstName, lastName, middleName);
            return Ok(result.Value);
        }

        [HttpGet("GetBySpecialization/{specId}")]
        public async Task<ActionResult<List<Doctor>>> GetBySpecialization(Guid specId)
        {
            var result = await _doctorService.GetDoctorListBySpecializationAsync(specId);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to retrieve doctors by specialization ID {SpecId}: {Error}", specId, result.Error);
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Retrieved doctors by specialization ID {SpecId}", specId);
            return Ok(result.Value);
        }
    }


}
