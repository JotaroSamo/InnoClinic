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

        public ReceptionistController(IReceptionistService receptionistService)
        {
            _receptionistService = receptionistService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Receptionist>> Create([FromBody] CreateReceptionistRequest createReceptionistRequest)
        {
            if (createReceptionistRequest == null)
            {
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
            var result = await _receptionistService.CreateReceptionistAsync(receptionist);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Receptionist>> GetById(Guid id)
        {
            var result = await _receptionistService.GetReceptionistByIdAsync(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Receptionist>>> GetAll()
        {
            var result = await _receptionistService.GetAllReceptionistsAsync();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Receptionist>> Update([FromBody] UpdateReceptionistRequest updateReceptionistRequest)
        {
            if (updateReceptionistRequest == null)
            {
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
            var result = await _receptionistService.UpdateReceptionistAsync(receptionist.Id, receptionist);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _receptionistService.DeleteReceptionistAsync(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return NoContent();
        }
    }
}
