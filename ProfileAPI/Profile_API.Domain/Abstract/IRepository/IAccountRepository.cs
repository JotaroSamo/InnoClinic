using CSharpFunctionalExtensions;
using Profile_API.Domain.Models;

namespace Profile_API.DataAccess.Repositories
{
    public interface IAccountRepository
    {
        Task<Result<Account>> CreateAccountAsync(Account account);
        Task<Result> DeleteAccountAsync(Guid id);
        Task<Result<Account>> GetAccountByIdAsync(Guid id);
        Task<Result<List<Account>>> GetAllAccountsAsync();
        Task<Result<Account>> UpdateAccountAsync(Guid id, Account account);
        Task<Result<Account>> VerificateEmail(Guid id);
    }
}