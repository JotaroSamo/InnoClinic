using CSharpFunctionalExtensions;
using Profile_API.Domain.Models;

namespace Profile_API.Application.Service
{
    public interface IAccountService
    {
        Task<Result<Account>> CreateAccountAsync(Account account);
        Task<Result> DeleteAccountAsync(Guid id);
        Task<Result<Account>> GetAccountByIdAsync(Guid id);
        Task<Result<List<Account>>> GetAllAccountsAsync();
        Task<Result<Account>> UpdateAccountAsync(Guid id, Account account);
        Task<Result> VerificateEmail(Guid id, string email);
    }
}