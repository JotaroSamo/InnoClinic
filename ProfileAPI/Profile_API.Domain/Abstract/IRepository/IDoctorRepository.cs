using CSharpFunctionalExtensions;
using Profile_API.Domain.Models;

namespace Profile_API.DataAccess.Repositories
{
    public interface IDoctorRepository
    {
        Task<Result<Doctor>> CreateDoctorAsync(Doctor doctor);
        Task<Result> DeleteDoctorAsync(Guid id);
        Task<Result<List<Doctor>>> GetAllDoctorsAsync();
        Task<Result<Doctor>> GetDoctorByIdAsync(Guid id);
        Task<Result<Doctor>> GetDoctorByNameAsync(string firstName, string lastName, string midleName);
        Task<Result<List<Doctor>>> GetDoctorListBySpecializationAsync(Guid specId);
        Task<Result<Doctor>> UpdateDoctorAsync(Guid id, Doctor doctor);
    }
}