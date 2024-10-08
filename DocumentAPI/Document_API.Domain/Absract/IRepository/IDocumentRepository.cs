using CSharpFunctionalExtensions;
using Document_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_API.Domain.Absract.IRepository
{
    public interface IDocumentRepository
    {
        Task<Result<List<Document>>> GetAll();
        Task<Result<Document>> GetById(Guid id);
        Task<Result<Document>> Create(Document document);
        Task<Result<Document>> Update(Document document);
        Task<Result> Delete(Guid id);
    }
}
