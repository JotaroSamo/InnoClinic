using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Profile_API.Application.Service;
using Profile_API.Contract.Request.Create;
using Profile_API.Contract.Request.Update;
using Profile_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Profile_API.Controllers
{
  

    [ApiController]
    [Route("api/[controller]")]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationService _specializationService;

        public SpecializationController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Specialization>> Create([FromBody] CreateSpecializationRequest createSpecializationRequest)
        {
            if (createSpecializationRequest == null)
            {
                return BadRequest("Specialization data is required.");
            }
            var specialization = new Specialization
            {
                SpecializationName = createSpecializationRequest.SpecializationName,
                IsActive = createSpecializationRequest.IsActive
            };

            var result = await _specializationService.CreateSpecializationAsync(specialization);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Specialization>> GetById(Guid id)
        {
            var result = await _specializationService.GetSpecializationByIdAsync(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Specialization>>> GetAll()
        {
            var result = await _specializationService.GetAllSpecializationsAsync();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Specialization>> Update([FromBody] UpdateSpecializationRequest updateSpecializationRequest)
        {
            if (updateSpecializationRequest == null)
            {
                return BadRequest("Specialization data is required.");
            }
            var specialization = new Specialization
            {
                Id = updateSpecializationRequest.Id,
                SpecializationName = updateSpecializationRequest.SpecializationName,
                IsActive = updateSpecializationRequest.IsActive
            };
            var result = await _specializationService.UpdateSpecializationAsync(updateSpecializationRequest.Id, specialization);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _specializationService.DeleteSpecializationAsync(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return NoContent();
        }
    }

}
