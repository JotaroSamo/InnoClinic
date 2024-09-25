using CSharpFunctionalExtensions;
using Profile_API.Domain.Models;

namespace Profile_API.DataAccess.Repositories
{
    public interface IPatientRepository
    {
        Task<Result<Patient>> CreatePatientAsync(Patient patient);
        Task<Result> DeletePatientAsync(int id);
        Task<Result<List<Patient>>> GetAllPatientsAsync();
        Task<Result<Patient>> GetPatientByIdAsync(int id);
        Task<Result<Patient>> UpdatePatientAsync(int id, Patient patient);
    }
}