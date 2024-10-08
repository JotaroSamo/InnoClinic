using CSharpFunctionalExtensions;
using Document_API.Domain.Model;

namespace Document_API.Application.Service
{
    public interface IDocumentService
    {
        Task<Result<Document>> CreateDocument(Document document);
        Task<Result> DeleteDocument(Guid id);
        Task<Result<List<Document>>> GetAllDocuments();
        Task<Result<Document>> GetDocumentById(Guid id);
        Task<Result<Document>> UpdateDocument(Document document);
    }
}