using CSharpFunctionalExtensions;
using Profile_API.Domain.Models;

namespace Profile_API.DataAccess.Repositories
{
    public interface IReceptionistRepository
    {
        Task<Result<Receptionist>> CreateReceptionistAsync(Receptionist receptionist);
        Task<Result> DeleteReceptionistAsync(Guid id);
        Task<Result<List<Receptionist>>> GetAllReceptionistsAsync();
        Task<Result<Receptionist>> GetReceptionistByIdAsync(Guid id);
        Task<Result<Receptionist>> UpdateReceptionistAsync(Guid id, Receptionist receptionist);
    }
}