using Microsoft.AspNetCore.Mvc;

namespace Profile_API.Controllers
{
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

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Doctor>> Create([FromBody] CreateDoctorRequest createDoctorRequest)
        {
            if (createDoctorRequest == null)
            {
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
            var result = await _doctorService.CreateDoctorAsync(doctor);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Doctor>> GetById(Guid id)
        {
            var result = await _doctorService.GetDoctorByIdAsync(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Doctor>>> GetAll()
        {
            var result = await _doctorService.GetAllDoctorsAsync();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Doctor>> Update([FromBody] UpdateDoctorRequest updateDoctorRequest)
        {
            if (updateDoctorRequest == null)
            {
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
            var result = await _doctorService.UpdateDoctorAsync(updateDoctorRequest.Id, doctor);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _doctorService.DeleteDoctorAsync(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return NoContent();
        }

        [HttpGet("GetByName")]
        public async Task<ActionResult<Doctor>> GetByName(string firstName, string lastName, string middleName)
        {
            var result = await _doctorService.GetDoctorByNameAsync(firstName, lastName, middleName);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("GetBySpecialization/{specId}")]
        public async Task<ActionResult<List<Doctor>>> GetBySpecialization(Guid specId)
        {
            var result = await _doctorService.GetDoctorListBySpecializationAsync(specId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
    }

}
