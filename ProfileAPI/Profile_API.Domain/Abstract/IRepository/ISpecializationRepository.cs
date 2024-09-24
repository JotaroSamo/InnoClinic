using Profile_API.Domain.Models;

namespace Profile_API.Domain.Abstract.IRepository;

public interface ISpecializationRepository
{
    Task<List<Specialization>> GetAllSpecializationsAsync();
    Task<Specialization> GetSpecializationByIdAsync(Guid id);
    Task<Specialization> CreateSpecializationAsync(Specialization specialization);
    Task<Specialization> UpdateSpecializationAsync(Guid id, Specialization specialization);
    Task DeleteSpecializationAsync(Guid id);
}