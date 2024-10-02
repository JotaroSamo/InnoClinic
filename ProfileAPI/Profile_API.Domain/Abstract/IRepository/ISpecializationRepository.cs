using CSharpFunctionalExtensions;
using Profile_API.Domain.Models;

namespace Profile_API.DataAccess.Repositories
{
    public interface ISpecializationRepository
    {
        Task<Result<Specialization>> Create(Specialization specialization);
        Task<Result> Delete(Guid id);
        Task<Result<List<Specialization>>> GetAll();
        Task<Result<Specialization>> GetById(Guid id);
        Task<Result<Specialization>> Update(Guid id, Specialization specialization);
    }
}