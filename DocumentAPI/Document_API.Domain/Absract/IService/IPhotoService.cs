using CSharpFunctionalExtensions;
using Document_API.Domain.Model;

namespace Document_API.Application.Service
{
    public interface IPhotoService
    {
        Task<Result<Photo>> CreatePhoto(Photo photo);
        Task<Result> DeletePhoto(Guid id);
        Task<Result<List<Photo>>> GetAllPhotos();
        Task<Result<Photo>> GetPhotoById(Guid id);
        Task<Result<Photo>> UpdatePhoto(Photo photo);
    }
}