using CSharpFunctionalExtensions;
using Profile_API.Domain.Models;

namespace Profile_API.DataAccess.Repositories
{
    public interface IReceptionistRepository
    {
        Task<Result<Receptionist>> Create(Receptionist receptionist);
        Task<Result> Delete(Guid id);
        Task<Result<List<Receptionist>>> GetAll();
        Task<Result<Receptionist>> GetById(Guid id);
        Task<Result<Receptionist>> Update(Guid id, Receptionist receptionist);
    }
}