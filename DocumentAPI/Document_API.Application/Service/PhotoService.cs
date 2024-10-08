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
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _repository;

        public PhotoService(IPhotoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Photo>> CreatePhoto(Photo photo)
        {
            return await _repository.Create(photo);
        }

        public async Task<Result> DeletePhoto(Guid id)
        {
            return await _repository.Delete(id);
        }

        public async Task<Result<List<Photo>>> GetAllPhotos()
        {
            return await _repository.GetAll();
        }

        public async Task<Result<Photo>> GetPhotoById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Result<Photo>> UpdatePhoto(Photo photo)
        {
            return await _repository.Update(photo);
        }
    }

}
