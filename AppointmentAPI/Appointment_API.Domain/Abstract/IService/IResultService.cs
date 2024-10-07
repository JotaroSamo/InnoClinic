using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;

namespace Appointment_API.Application.Service
{
    public interface IResultService
    {
        Task<Result<Results>> CreateResult(Results result);
        Task<Result> DeleteResult(Guid id);
        Task<Result<List<Results>>> GetAllResults();
        Task<Result<Results>> GetResultById(Guid id);
        Task<Result<Results>> UpdateResult(Results result);
    }
}