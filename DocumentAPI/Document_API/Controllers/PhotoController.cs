using Document_API.Application.Service;
using Document_API.Contract.Request.Create;
using Document_API.Contract.Request.Update;
using Document_API.Domain.Model;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Document_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService; // Убедитесь, что у вас есть IPhotoService
        private readonly IValidator<Photo> _photoValidator;
        private readonly ILogger<PhotoController> _logger;

        public PhotoController(IPhotoService photoService,
                               IValidator<Photo> photoValidator,
                               ILogger<PhotoController> logger)
        {
            _photoService = photoService;
            _photoValidator = photoValidator;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all photos."); // Логируем начало операции

            var result = await _photoService.GetAllPhotos(); // Предполагается, что этот метод существует
            if (result.IsFailure)
            {
                _logger.LogError("Failed to retrieve photos: {Error}", result.Error); // Логируем ошибку
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Retrieved {Count} photos.", result.Value.Count); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Getting photo by ID: {Id}", id); // Логируем начало операции

            var result = await _photoService.GetPhotoById(id); // Предполагается, что этот метод существует
            if (result.IsFailure)
            {
                _logger.LogWarning("Photo not found: {Id}. Error: {Error}", id, result.Error); // Логируем предупреждение
                return NotFound(result.Error);
            }

            _logger.LogInformation("Retrieved photo: {Photo}", result.Value); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] PhotoCreateRequest request)
        {
            var photo = new Photo
            {
                Id = Guid.NewGuid(),
                Url = request.Url,
         
            };

            var validationResult = await _photoValidator.ValidateAsync(photo);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for creating photo: {Errors}", validationResult.Errors); // Логируем ошибку валидации
                return BadRequest(validationResult.Errors);
            }

            var result = await _photoService.CreatePhoto(photo); // Предполагается, что этот метод существует
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create photo: {Error}", result.Error); // Логируем ошибку
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Created photo: {Photo}", result.Value); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] PhotoUpdateRequest request)
        {
            var photo = new Photo
            {
                Id = request.Id,
                Url = request.Url,
             
            };

            var validationResult = await _photoValidator.ValidateAsync(photo);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for updating photo: {Errors}", validationResult.Errors); // Логируем ошибку валидации
                return BadRequest(validationResult.Errors);
            }

            var result = await _photoService.UpdatePhoto(photo); // Предполагается, что этот метод существует
            if (result.IsFailure)
            {
                _logger.LogWarning("Photo not found for update: {Id}. Error: {Error}", photo.Id, result.Error); // Логируем предупреждение
                return NotFound(result.Error);
            }

            _logger.LogInformation("Updated photo: {Photo}", result.Value); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Deleting photo with ID: {Id}", id); // Логируем начало операции

            var result = await _photoService.DeletePhoto(id); // Предполагается, что этот метод существует
            if (result.IsFailure)
            {
                _logger.LogWarning("Photo not found for deletion: {Id}. Error: {Error}", id, result.Error); // Логируем предупреждение
                return NotFound(result.Error);
            }

            _logger.LogInformation("Deleted photo with ID: {Id}", id); // Логируем успешное завершение
            return NoContent();
        }
    }
}
