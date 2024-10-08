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
    public class PhotoRepository : IPhotoRepository
    {
        private readonly IMongoCollection<PhotoEntity> _context;
        private readonly IMapper _mapper;

        public PhotoRepository(DocumentDbContext context, IMapper mapper)
        {
            _context = context.Photos;
            _mapper = mapper;
        }

        public async Task<Result<Photo>> Create(Photo photo)
        {
            try
            {
                var photoEntity = _mapper.Map<PhotoEntity>(photo);
                await _context.InsertOneAsync(photoEntity);
                return Result.Success(photo);
            }
            catch (Exception ex)
            {
                return Result.Failure<Photo>(ex.ToString());
            }
        }

        public async Task<Result> Delete(Guid id)
        {
            try
            {
                var filter = Builders<PhotoEntity>.Filter.Eq(photo => photo.Id, id);
                var deleteResult = await _context.DeleteOneAsync(filter);

                if (deleteResult.DeletedCount == 0)
                    return Result.Failure($"Photo with ID {id} not found.");

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.ToString());
            }
        }

        public async Task<Result<List<Photo>>> GetAll()
        {
            try
            {
                var photoEntities = await _context.Find(_ => true).ToListAsync();
                var photos = _mapper.Map<List<Photo>>(photoEntities);
                return Result.Success(photos);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<Photo>>(ex.ToString());
            }
        }

        public async Task<Result<Photo>> GetById(Guid id)
        {
            try
            {
                var filter = Builders<PhotoEntity>.Filter.Eq(photo => photo.Id, id);
                var photoEntity = await _context.Find(filter).FirstOrDefaultAsync();

                if (photoEntity == null)
                    return Result.Failure<Photo>($"Photo with ID {id} not found.");

                var photo = _mapper.Map<Photo>(photoEntity);
                return Result.Success(photo);
            }
            catch (Exception ex)
            {
                return Result.Failure<Photo>(ex.ToString());
            }
        }

        public async Task<Result<Photo>> Update(Photo photo)
        {
            try
            {
                var filter = Builders<PhotoEntity>.Filter.Eq(photo => photo.Id, photo.Id);
                var photoEntity = _mapper.Map<PhotoEntity>(photo);

                var updateResult = await _context.ReplaceOneAsync(filter, photoEntity);

                if (updateResult.ModifiedCount == 0)
                    return Result.Failure<Photo>($"Photo with ID {photo.Id} not found or not updated.");

                return Result.Success(photo);
            }
            catch (Exception ex)
            {
                return Result.Failure<Photo>(ex.ToString());
            }
        }
    }
}
