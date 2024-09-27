
using CSharpFunctionalExtensions;
using Profile_API.DataAccess.Repositories;
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

        public async Task<Result<Account>> CreateAccountAsync(Account account)
        {
            var creationResult = await _accountRepository.Create(account);
            if (creationResult.IsFailure)
                return Result.Failure<Account>("Failed to create account");

            await _emailService.SendConfirmationLink(creationResult.Value.Email, creationResult.Value.Id);
            return Result.Success(creationResult.Value);
        }

        public async Task<Result> DeleteAccountAsync(Guid id)
        {
            var deleteResult = await _accountRepository.Delete(id);
            if (deleteResult.IsFailure)
                return Result.Failure("Failed to delete account");

            return Result.Success();
        }

        public async Task<Result<Account>> GetAccountByIdAsync(Guid id)
        {
            var accountResult = await _accountRepository.GetById(id);
            if (accountResult.IsFailure)
                return Result.Failure<Account>("Account not found");

            return Result.Success(accountResult.Value);
        }

        public async Task<Result<List<Account>>> GetAllAccountsAsync()
        {
            var accountsResult = await _accountRepository.GetAll();
            return Result.Success(accountsResult.Value);
        }

        public async Task<Result<Account>> UpdateAccountAsync(Guid id, Account account)
        {
            var updateResult = await _accountRepository.Update(id, account);
            if (updateResult.IsFailure)
                return Result.Failure<Account>("Failed to update account");

            return Result.Success(updateResult.Value);
        }

        public async Task<Result> VerificateEmail(Guid id, string email)
        {
            await _emailService.SendCredentialsToEmail(email);
           

            var verificationResult = await _accountRepository.VerificateEmail(id);
            if (verificationResult.IsFailure)
                return Result.Failure("Email verification failed");

            return Result.Success();
        }
    }

}
