using Profile_API.Domain.Abstract.IRepository;
using Profile_API.Domain.Abstract.IService;
using Profile_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile_API.Application.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Account> CreateAccountAsync(Account account)
        {
            return await _accountRepository.CreateAccountAsync(account);
        }

        public async Task DeleteAccountAsync(Guid id)
        {
            await _accountRepository.DeleteAccountAsync(id);
        }

        public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            return await _accountRepository.GetAccountByIdAsync(id);
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _accountRepository.GetAllAccountsAsync();
        }

        public async Task<Account> UpdateAccountAsync(Guid id, Account account)
        {
           return await _accountRepository.UpdateAccountAsync(id, account);
        }
    }
}
