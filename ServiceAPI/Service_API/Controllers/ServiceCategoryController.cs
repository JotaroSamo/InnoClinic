using FluentValidation;
using Global.Dto;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Service_API.Contract.Request.Create;
using Service_API.Contract.Request.Update;
using Service_API.Domain.Abstract.IService;
using Service_API.Domain.Model;
using Service_API.Infrastructure.Validator;

namespace Service_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceCategoryController : ControllerBase
    {
        private readonly IServiceCategoryService _serviceCategoryService;
        private readonly IValidator<ServiceCategory> _serviceCategoryValidator;
        private readonly ILogger<ServiceCategoryController> _logger; // Добавляем ILogger


        public ServiceCategoryController(IServiceCategoryService serviceCategoryService,
                                         IValidator<ServiceCategory> serviceCategoryValidator,
                                         ILogger<ServiceCategoryController> logger) // Внедряем ILogger
        {
            _serviceCategoryService = serviceCategoryService;
            _serviceCategoryValidator = serviceCategoryValidator;
            _logger = logger; // Инициализируем ILogger
      
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all service categories."); // Логируем начало операции

            var result = await _serviceCategoryService.GetAllCategoryService();
            if (result.IsFailure)
            {
                _logger.LogError("Failed to retrieve categories: {Error}", result.Error); // Логируем ошибку
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Retrieved {Count} service categories.", result.Value.Count); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Getting service category by ID: {Id}", id); // Логируем начало операции

            var result = await _serviceCategoryService.GetByIdCategoryService(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Service category not found: {Id}. Error: {Error}", id, result.Error); // Логируем предупреждение
                return NotFound(result.Error);
            }

            _logger.LogInformation("Retrieved service category: {Category}", result.Value); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateServiceCategoryRequest request)
        {
            var serviceCategory = new ServiceCategory
            {
                Id = Guid.NewGuid(),
                CategoryName = request.CategoryName,
                TimeSlotSize = request.TimeSlotSize
            };

            var validationResult = await _serviceCategoryValidator.ValidateAsync(serviceCategory);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for creating service category: {Errors}", validationResult.Errors); // Логируем ошибку валидации
                return BadRequest(validationResult.Errors);
            }

            var result = await _serviceCategoryService.CreateCategoryService(serviceCategory);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create service category: {Error}", result.Error); // Логируем ошибку
                return BadRequest(result.Error);
            }
           
            _logger.LogInformation("Created service category: {Category}", result.Value); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateServiceCategoryRequest request)
        {
            var serviceCategory = new ServiceCategory
            {
                Id = request.Id,
                CategoryName = request.CategoryName,
                TimeSlotSize = request.TimeSlotSize
            };

            var validationResult = await _serviceCategoryValidator.ValidateAsync(serviceCategory);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for updating service category: {Errors}", validationResult.Errors); // Логируем ошибку валидации
                return BadRequest(validationResult.Errors);
            }

            var result = await _serviceCategoryService.UpdateCategoryService(serviceCategory);
            if (result.IsFailure)
            {
                _logger.LogWarning("Service category not found for update: {Id}. Error: {Error}", request.Id, result.Error); // Логируем предупреждение
                return NotFound(result.Error);
            }

            _logger.LogInformation("Updated service category: {Category}", result.Value); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Deleting service category with ID: {Id}", id); // Логируем начало операции

            var result = await _serviceCategoryService.DeleteCategoryService(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Service category not found for deletion: {Id}. Error: {Error}", id, result.Error); // Логируем предупреждение
                return NotFound(result.Error);
            }

            _logger.LogInformation("Deleted service category with ID: {Id}", id); // Логируем успешное завершение
            return NoContent();
        }
    }
}
