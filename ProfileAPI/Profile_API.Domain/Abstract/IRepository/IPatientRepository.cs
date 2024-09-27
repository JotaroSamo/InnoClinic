using CSharpFunctionalExtensions;
using Profile_API.Domain.Models;

namespace Profile_API.DataAccess.Repositories
{
    public interface IPatientRepository
    {
        Task<Result<Patient>> Create(Patient patient);
        Task<Result> Delete(Guid id);
        Task<Result<List<Patient>>> GetAll();
        Task<Result<Patient>> GetById(Guid id);
        Task<Result<Patient>> Update(Guid id, Patient patient);
    }
}