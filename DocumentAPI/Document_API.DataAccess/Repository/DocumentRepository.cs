using AutoMapper;
using CSharpFunctionalExtensions;
using Document_API.DataAccess.Entity;
using Document_API.Domain.Absract.IRepository;
using Document_API.Domain.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_API.DataAccess.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IMongoCollection<DocumentEntity> _context;
        private readonly IMapper _mapper;

        public DocumentRepository(DocumentDbContext context, IMapper mapper)
        {
            _context = context.Documents;
            _mapper = mapper;
        }

        public async Task<Result<Document>> Create(Document document)
        {
            try
            {
                var documentEntity = _mapper.Map<DocumentEntity>(document);
                await _context.InsertOneAsync(documentEntity);
                return Result.Success(document);
            }
            catch (Exception ex)
            {
                return Result.Failure<Document>(ex.ToString());
            }
        }

        public async Task<Result> Delete(Guid id)
        {
            try
            {
                var filter = Builders<DocumentEntity>.Filter.Eq(doc => doc.Id, id);
                var deleteResult = await _context.DeleteOneAsync(filter);

                if (deleteResult.DeletedCount == 0)
                    return Result.Failure($"Document with ID {id} not found.");

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.ToString());
            }
        }

        public async Task<Result<List<Document>>> GetAll()
        {
            try
            {
                var documentEntities = await _context.Find(_ => true).ToListAsync();
                var documents = _mapper.Map<List<Document>>(documentEntities);
                return Result.Success(documents);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<Document>>(ex.ToString());
            }
        }

        public async Task<Result<Document>> GetById(Guid id)
        {
            try
            {
                var filter = Builders<DocumentEntity>.Filter.Eq(doc => doc.Id, id);
                var documentEntity = await _context.Find(filter).FirstOrDefaultAsync();

                if (documentEntity == null)
                    return Result.Failure<Document>($"Document with ID {id} not found.");

                var document = _mapper.Map<Document>(documentEntity);
                return Result.Success(document);
            }
            catch (Exception ex)
            {
                return Result.Failure<Document>(ex.ToString());
            }
        }

        public async Task<Result<Document>> Update(Document document)
        {
            try
            {
                var filter = Builders<DocumentEntity>.Filter.Eq(doc => doc.Id, document.Id);
                var documentEntity = _mapper.Map<DocumentEntity>(document);

                var updateResult = await _context.ReplaceOneAsync(filter, documentEntity);

                if (updateResult.ModifiedCount == 0)
                    return Result.Failure<Document>($"Document with ID {document.Id} not found or not updated.");

                return Result.Success(document);
            }
            catch (Exception ex)
            {
                return Result.Failure<Document>(ex.ToString());
            }
        }
    }
}
