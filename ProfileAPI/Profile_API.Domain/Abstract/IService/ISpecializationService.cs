using CSharpFunctionalExtensions;
using Profile_API.Domain.Models;

namespace Profile_API.Application.Service
{
    public interface ISpecializationService
    {
        Task<Result<Specialization>> CreateSpecializationAsync(Specialization specialization);
        Task<Result> DeleteSpecializationAsync(Guid id);
        Task<Result<List<Specialization>>> GetAllSpecializationsAsync();
        Task<Result<Specialization>> GetSpecializationByIdAsync(Guid id);
        Task<Result<Specialization>> UpdateSpecializationAsync(Guid id, Specialization specialization);
    }
}