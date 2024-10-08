using CSharpFunctionalExtensions;
using Document_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_API.Domain.Absract.IRepository
{
    public interface IPhotoRepository
    {
        Task<Result<Photo>> Create(Photo photo);
        Task<Result> Delete(Guid id);
        Task<Result<List<Photo>>> GetAll();
        Task<Result<Photo>> GetById(Guid id);
        Task<Result<Photo>> Update(Photo photo);
    }
}
