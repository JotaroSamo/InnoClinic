using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Res = Appointment_API.Domain.Model.Results;
using Appointment_API.Contract.Request.Update;
using Appointment_API.DataAccess.IService;
using Appointment_API.Application.Service;
using Appointment_API.Contract.Request.Create;

namespace Appointment_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly IResultService _resultsService; // Сервис для работы с результатами
        private readonly IValidator<Res> _resultsValidator; // Валидатор для Results
        private readonly ILogger<ResultsController> _logger; // Логгер

        public ResultsController(IResultService resultsService,
                                 IValidator<Res> resultsValidator,
                                 ILogger<ResultsController> logger)
        {
            _resultsService = resultsService;
            _resultsValidator = resultsValidator;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all results."); // Логируем начало операции

            var result = await _resultsService.GetAllResults();
            if (result.IsFailure)
            {
                _logger.LogError("Failed to retrieve results: {Error}", result.Error); // Логируем ошибку
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Retrieved {Count} results.", result.Value.Count); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Getting result by ID: {Id}", id); // Логируем начало операции

            var result = await _resultsService.GetResultById(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Result not found: {Id}. Error: {Error}", id, result.Error); // Логируем предупреждение
                return NotFound(result.Error);
            }

            _logger.LogInformation("Retrieved result: {Result}", result.Value); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateResultRequest request)
        {
            var resultEntity = new Res
            {
                
                Complaints = request.Complaints,
                Conclusion = request.Conclusion,
                Recommendations = request.Recommendations,
                AppointmentId = request.AppointmentId
            };

            var validationResult = await _resultsValidator.ValidateAsync(resultEntity);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for creating result: {Errors}", validationResult.Errors); // Логируем ошибку валидации
                return BadRequest(validationResult.Errors);
            }

            var result = await _resultsService.CreateResult(resultEntity);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create result: {Error}", result.Error); // Логируем ошибку
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Created result: {Result}", result.Value); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateResultRequest request)
        {
            var resultEntity = new Res
            {
                Id = request.Id,
                Complaints = request.Complaints,
                Conclusion = request.Conclusion,
                Recommendations = request.Recommendations,
                AppointmentId = request.AppointmentId
            };

            var validationResult = await _resultsValidator.ValidateAsync(resultEntity);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for updating result: {Errors}", validationResult.Errors); // Логируем ошибку валидации
                return BadRequest(validationResult.Errors);
            }

            var result = await _resultsService.UpdateResult(resultEntity);
            if (result.IsFailure)
            {
                _logger.LogWarning("Result not found for update: {Id}. Error: {Error}", request.Id, result.Error); // Логируем предупреждение
                return NotFound(result.Error);
            }

            _logger.LogInformation("Updated result: {Result}", result.Value); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Deleting result with ID: {Id}", id); // Логируем начало операции

            var result = await _resultsService.DeleteResult(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Result not found for deletion: {Id}. Error: {Error}", id, result.Error); // Логируем предупреждение
                return NotFound(result.Error);
            }

            _logger.LogInformation("Deleted result with ID: {Id}", id); // Логируем успешное завершение
            return NoContent();
        }
    }

}
