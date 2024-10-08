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
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IValidator<Document> _documentValidator;
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(IDocumentService documentService,
                                  IValidator<Document> documentValidator,
                                  ILogger<DocumentController> logger)
        {
            _documentService = documentService;
            _documentValidator = documentValidator;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all documents."); // Логируем начало операции

            var result = await _documentService.GetAllDocuments();
            if (result.IsFailure)
            {
                _logger.LogError("Failed to retrieve documents: {Error}", result.Error); // Логируем ошибку
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Retrieved {Count} documents.", result.Value.Count); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Getting document by ID: {Id}", id); // Логируем начало операции

            var result = await _documentService.GetDocumentById(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Document not found: {Id}. Error: {Error}", id, result.Error); // Логируем предупреждение
                return NotFound(result.Error);
            }

            _logger.LogInformation("Retrieved document: {Document}", result.Value); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] DocumentCreateRequest request)
        {

            var document = new Document
            {
                Id = Guid.NewGuid(),
                Url = request.Url,
                ResultId = request.ResultId,
                Recommendations = request.Recommendations,
                Complaints = request.Complaints,
                Conclusion = request.Conclusion
            };
            var validationResult = await _documentValidator.ValidateAsync(document);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for creating document: {Errors}", validationResult.Errors); // Логируем ошибку валидации
                return BadRequest(validationResult.Errors);
            }
           
            var result = await _documentService.CreateDocument(document);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create document: {Error}", result.Error); // Логируем ошибку
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Created document: {Document}", result.Value); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] DocumentUpdateRequest request)
        {
            var document = new Document
            {
                Id = request.Id,
                Url = request.Url,
                ResultId = request.ResultId,
                Recommendations = request.Recommendations,
                Complaints = request.Complaints,
                Conclusion = request.Conclusion
            };
            var validationResult = await _documentValidator.ValidateAsync(document);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for updating document: {Errors}", validationResult.Errors); // Логируем ошибку валидации
                return BadRequest(validationResult.Errors);
            }

            var result = await _documentService.UpdateDocument(document);
            if (result.IsFailure)
            {
                _logger.LogWarning("Document not found for update: {Id}. Error: {Error}", document.Id, result.Error); // Логируем предупреждение
                return NotFound(result.Error);
            }

            _logger.LogInformation("Updated document: {Document}", result.Value); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Deleting document with ID: {Id}", id); // Логируем начало операции

            var result = await _documentService.DeleteDocument(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Document not found for deletion: {Id}. Error: {Error}", id, result.Error); // Логируем предупреждение
                return NotFound(result.Error);
            }

            _logger.LogInformation("Deleted document with ID: {Id}", id); // Логируем успешное завершение
            return NoContent();
        }
    }
}
