using CSharpFunctionalExtensions;
using Profile_API.Domain.Models;

namespace Profile_API.DataAccess.Repositories
{
    public interface IAccountRepository
    {
        Task<Result<Account>> Create(Account account);
        Task<Result> Delete(Guid id);
        Task<Result<Account>> GetById(Guid id);
        Task<Result<List<Account>>> GetAll();
        Task<Result<Account>> Update(Guid id, Account account);
        Task<Result<Account>> VerificateEmail(Guid id);
    }
}