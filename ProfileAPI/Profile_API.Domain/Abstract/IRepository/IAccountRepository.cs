using Profile_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile_API.Domain.Abstract.IRepository
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAllAccountsAsync();
        Task<Account> GetAccountByIdAsync(Guid id);
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> UpdateAccountAsync(Guid id, Account account);
        Task DeleteAccountAsync(Guid id);

        Task<Account> VerificateEmail(Guid id);
    }
}
