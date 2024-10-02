using FluentValidation;
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
        private readonly ILogger<SpecializationController> _logger; // Логгер для логирования действий

        public SpecializationController(ISpecializationService specializationService, ILogger<SpecializationController> logger)
        {
            _specializationService = specializationService;
            _logger = logger;
        }

        //[HttpPost("Create")]
        //public async Task<ActionResult<Specialization>> Create([FromBody] CreateSpecializationRequest createSpecializationRequest)
        //{
        //    if (createSpecializationRequest == null)
        //    {
        //        _logger.LogWarning("CreateSpecializationRequest is null.");
        //        return BadRequest("Specialization data is required.");
        //    }

        //    var specialization = new Specialization
        //    {
        //        SpecializationName = createSpecializationRequest.SpecializationName,
        //        IsActive = createSpecializationRequest.IsActive
        //    };

        //   

        //    var result = await _specializationService.CreateSpecializationAsync(specialization);
        //    if (result.IsFailure)
        //    {
        //        _logger.LogError("Failed to create specialization: {Error}", result.Error);
        //        return BadRequest(result.Error);
        //    }

        //    _logger.LogInformation("Specialization created successfully: {SpecializationId}", result.Value.Id);
        //    return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        //}

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Specialization>> GetById(Guid id)
        {
            var result = await _specializationService.GetSpecializationByIdAsync(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Specialization with ID {Id} not found: {Error}", id, result.Error);
                return NotFound(result.Error);
            }

            _logger.LogInformation("Retrieved specialization with ID {Id}", id);
            return Ok(result.Value);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Specialization>>> GetAll()
        {
            var result = await _specializationService.GetAllSpecializationsAsync();
            if (result.IsFailure)
            {
                _logger.LogError("Failed to retrieve specializations: {Error}", result.Error);
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Retrieved all specializations");
            return Ok(result.Value);
        }

        //[HttpPut("Update")]
        //public async Task<ActionResult<Specialization>> Update([FromBody] UpdateSpecializationRequest updateSpecializationRequest)
        //{
        //    if (updateSpecializationRequest == null)
        //    {
        //        _logger.LogWarning("UpdateSpecializationRequest is null.");
        //        return BadRequest("Specialization data is required.");
        //    }

        //    var specialization = new Specialization
        //    {
        //        Id = updateSpecializationRequest.Id,
        //        SpecializationName = updateSpecializationRequest.SpecializationName,
        //        IsActive = updateSpecializationRequest.IsActive
        //    };

        //    var result = await _specializationService.UpdateSpecializationAsync(updateSpecializationRequest.Id, specialization);
        //    if (result.IsFailure)
        //    {
        //        _logger.LogError("Failed to update specialization: {Error}", result.Error);
        //        return BadRequest(result.Error);
        //    }

        //    _logger.LogInformation("Specialization updated successfully: {SpecializationId}", result.Value.Id);
        //    return Ok(result.Value);
        //}

        //[HttpDelete("Delete/{id}")]
        //public async Task<ActionResult> Delete(Guid id)
        //{
        //    var result = await _specializationService.DeleteSpecializationAsync(id);
        //    if (result.IsFailure)
        //    {
        //        _logger.LogWarning("Failed to delete specialization with ID {Id}: {Error}", id, result.Error);
        //        return NotFound(result.Error);
        //    }

        //    _logger.LogInformation("Specialization with ID {Id} deleted successfully", id);
        //    return NoContent();
        //}
    }


}
