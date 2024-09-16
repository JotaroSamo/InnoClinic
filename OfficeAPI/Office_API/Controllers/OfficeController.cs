using Microsoft.AspNetCore.Mvc;
using Office_API.Domain.Abstract.Service;
using Microsoft.Extensions.Logging;
using Office_API.Contract;
using Office_API.Domain.Enums;

using System;

using System.Threading.Tasks;
namespace Office_API.Controllers
{

    

    [ApiController]
    [Route("api/[controller]")]
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeService _officeService;
        private readonly ILogger<OfficeController> _logger;

        public OfficeController(IOfficeService officeService, ILogger<OfficeController> logger)
        {
            _officeService = officeService;
            _logger = logger;
        }

        [HttpPost("New Office")]
        public async Task<IActionResult> AddOffice([FromBody] OfficeCreateRequest officeRequest)
        {
            if (officeRequest == null)
            {
                return BadRequest("Office cannot be null.");
            }
            var result = await _officeService.AddOffice(officeRequest.City, 
                officeRequest.Street, officeRequest.HouseNumber, 
                officeRequest.OfficeNumber,officeRequest.RegistryPhoneNumber ,officeRequest.IsActive,
                photoUrl: officeRequest.PhotoUrl);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            _logger.LogError(result.Error);
            return BadRequest(result.Error);
        }

        [HttpPost("Change-status/{id}")]
        public async Task<IActionResult> ChangeStatusOffice(Guid id, [FromForm] Status isActive)
        {
            var result = await _officeService.ChangeStatusOffice(id, isActive);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            _logger.LogError(result.Error);
            return NotFound(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffice(Guid id)
        {
            var result = await _officeService.DeleteteOffice(id);

            if (result.IsSuccess)
            {
                return NoContent();
            }

            _logger.LogError(result.Error);
            return NotFound(result.Error);
        }

        [HttpGet("GetAllOffices")]
        public async Task<IActionResult> GetAllOffices()
        {
            var result = await _officeService.GetAllOffices();

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            _logger.LogError(result.Error);
            return StatusCode(500, result.Error);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByIdOffice(Guid id)
        {
            var result = await _officeService.GetByIdOffice(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            _logger.LogError(result.Error);
            return NotFound(result.Error);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateOffice([FromForm] OfficeUpdateRequest officeRequest)
        {
            if (officeRequest == null)
            {
                return BadRequest("Office cannot be null.");
            }

            var result = await _officeService.UpdateOffice(officeRequest.Id,officeRequest.City,
                officeRequest.Street, officeRequest.HouseNumber,
                officeRequest.OfficeNumber, officeRequest.RegistryPhoneNumber, officeRequest.IsActive,
                photoUrl: officeRequest.PhotoUrl);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            _logger.LogError(result.Error);
            return NotFound(result.Error);
        }
    }

}
