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

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Patient>> Create([FromBody]CreatePatientRequest createPatientRequest)
        {
            if (createPatientRequest == null)
            {
                return BadRequest("Patient data is required.");
            }
            var patient = new Patient { 
            FirstName = createPatientRequest.FirstName,
            LastName = createPatientRequest.LastName,
            MiddleName = createPatientRequest.MiddleName,
            IsLinkedToAccount = createPatientRequest.IsLinkedToAccount,
            DateOfBirth = createPatientRequest.DateOfBirth,
            AccountId = createPatientRequest.AccountId
            };
            var result = await _patientService.CreatePatientAsync(patient);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Patient>> GetById(Guid id)
        {
            var result = await _patientService.GetPatientByIdAsync(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Patient>>> GetAll()
        {
            var result = await _patientService.GetAllPatientsAsync();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Patient>> Update([FromBody] UpdatePatientRequest updatePatientRequest)
        {
            if (updatePatientRequest == null)
            {
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
            var result = await _patientService.UpdatePatientAsync(patient.Id, patient);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _patientService.DeletePatientAsync(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return NoContent();
        }
    }
}
