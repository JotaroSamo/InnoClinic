using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Profile_API.DataAccess.Entity;
using Profile_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile_API.DataAccess.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ProfileDbContext _context;
        private readonly IMapper _mapper;

        public AccountRepository(ProfileDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<Account>>> GetAllAccountsAsync()
        {
            var accountsEntities = await _context.Accounts
                .Include(d => d.Doctor)
                .Include(p => p.Patient)
                .Include(r => r.Receptionist).AsNoTracking()
                .ToListAsync();

            var accounts = _mapper.Map<List<Account>>(accountsEntities);
            return Result.Success(accounts);
        }

        public async Task<Result<Account>> GetAccountByIdAsync(Guid id)
        {
            var accountEntity = await _context.Accounts
                .Include(d => d.Doctor)
                .Include(p => p.Patient)
                .Include(r => r.Receptionist).AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);

            if (accountEntity == null)
                return Result.Failure<Account>("Account not found");

            var account = _mapper.Map<Account>(accountEntity);
            return Result.Success(account);
        }

        public async Task<Result<Account>> CreateAccountAsync(Account account)
        {
            var accountEntity = _mapper.Map<AccountEntity>(account);
            accountEntity.CreatedAt = DateTime.UtcNow;
            accountEntity.UpdatedAt = DateTime.UtcNow;

            _context.Accounts.Add(accountEntity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

               return Result.Failure<Account>(ex.Message);
            }
          

            var createdAccount = _mapper.Map<Account>(accountEntity);
            return Result.Success(createdAccount);
        }

        public async Task<Result<Account>> UpdateAccountAsync(Guid id, Account account)
        {
            var accountEntity = await _context.Accounts
                .Include(d => d.Doctor)
                .Include(p => p.Patient)
                .Include(r => r.Receptionist)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (accountEntity == null)
                return Result.Failure<Account>("Account not found");

            _mapper.Map(account, accountEntity);
            accountEntity.UpdatedAt = DateTime.UtcNow;

            _context.Accounts.Update(accountEntity);
            await _context.SaveChangesAsync();

            var updatedAccount = _mapper.Map<Account>(accountEntity);
            return Result.Success(updatedAccount);
        }

        public async Task<Result> DeleteAccountAsync(Guid id)
        {
            var accountEntity = await _context.Accounts
                .Include(d => d.Doctor)
                .Include(p => p.Patient)
                .Include(r => r.Receptionist)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (accountEntity == null)
                return Result.Failure("Account not found");

            _context.Accounts.Remove(accountEntity);
            await _context.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result<Account>> VerificateEmail(Guid id)
        {
            var accountEntity = await _context.Accounts.FindAsync(id);
            if (accountEntity == null)
                return Result.Failure<Account>("Account not found");

            accountEntity.IsEmailVerified = true;
            await _context.SaveChangesAsync();

            var verifiedAccount = _mapper.Map<Account>(accountEntity);
            return Result.Success(verifiedAccount);
        }
    }

}
