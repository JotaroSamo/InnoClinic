using CSharpFunctionalExtensions;
using Profile_API.Domain.Models;

namespace Profile_API.DataAccess.Repositories
{
    public interface IDoctorRepository
    {
        Task<Result<Doctor>> Create(Doctor doctor);
        Task<Result> Delete(Guid id);
        Task<Result<List<Doctor>>> GetAll();
        Task<Result<Doctor>> GetById(Guid id);
        Task<Result<Doctor>> GetByName(string firstName, string lastName, string midleName);
        Task<Result<List<Doctor>>> GetBySpecialization(Guid specId);
        Task<Result<Doctor>> Update(Guid id, Doctor doctor);
    }
}