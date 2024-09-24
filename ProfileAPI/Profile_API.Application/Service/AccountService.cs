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
        private readonly IEmailService _emailService;

        public AccountService(IAccountRepository accountRepository, IEmailService emailService)
        {
            _accountRepository = accountRepository;
            _emailService = emailService;
        }
        public async Task<Account> CreateAccountAsync(Account account)
        {
            account = await _accountRepository.CreateAccountAsync(account);
            var verificationLink = $"https://localhost:7246/api/account/verify-email?userId={account.Id}";

            await _emailService.SendVerificationEmail(account.Email, verificationLink);
            return account;
            
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

        public async Task<Account> VerificateEmail(Guid id)
        {
          return await _accountRepository.VerificateEmail(id);
        }
    }
}
