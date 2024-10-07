using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;

namespace Appointment_API.DataAccess.Repository
{
    public interface IResultRepository
    {
        Task<Result<Results>> Create(Results result);
        Task<Result> Delete(Guid id);
        Task<Result<List<Results>>> GetAll();
        Task<Result<Results>> GetById(Guid id);
        Task<Result<Results>> Update(Results result);
    }
}