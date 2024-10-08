using CSharpFunctionalExtensions;
using Document_API.Domain.Absract.IRepository;
using Document_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_API.Application.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repository;

        public DocumentService(IDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Document>> CreateDocument(Document document)
        {
            return await _repository.Create(document);
        }

        public async Task<Result> DeleteDocument(Guid id)
        {
            return await _repository.Delete(id);
        }

        public async Task<Result<List<Document>>> GetAllDocuments()
        {
            return await _repository.GetAll();
        }

        public async Task<Result<Document>> GetDocumentById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Result<Document>> UpdateDocument(Document document)
        {
            return await _repository.Update(document);
        }
    }

}
