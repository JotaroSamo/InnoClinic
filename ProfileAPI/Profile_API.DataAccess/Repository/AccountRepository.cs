using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Profile_API.DataAccess.Entity;
using Profile_API.Domain.Abstract;
using Profile_API.Domain.Abstract.IRepository;
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

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            var accountsEntities = await _context.Accounts.Include(d => d.Doctor).Include(p => p.Patient).Include(r => r.Receptionist).ToListAsync();
            return _mapper.Map<List<Account>>(accountsEntities);
        }

        public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            var accountEntity = await _context.Accounts.Include(d=>d.Doctor).Include(p => p.Patient).Include(r => r.Receptionist).FirstOrDefaultAsync(i=>i.Id == id);
            if (accountEntity == null) throw new Exception("Account not found");
            return _mapper.Map<Account>(accountEntity);
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            var accountEntity = _mapper.Map<AccountEntity>(account);
            accountEntity.CreatedAt = DateTime.UtcNow;
            accountEntity.UpdatedAt = DateTime.UtcNow;

            _context.Accounts.Add(accountEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<Account>(accountEntity);
        }

        public async Task<Account> UpdateAccountAsync(Guid id, Account account)
        {
            var accountEntity = await _context.Accounts.Include(d => d.Doctor).Include(p => p.Patient).Include(r => r.Receptionist).FirstOrDefaultAsync(i => i.Id == id);
            if (account == null) throw new Exception("Account not found");

            _mapper.Map(account, account);
            account.UpdatedAt = DateTime.UtcNow;

            _context.Accounts.Update(accountEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<Account>(account);
        }

        public async Task DeleteAccountAsync(Guid id)
        {
            var accountEntity = await _context.Accounts.Include(d => d.Doctor).Include(p => p.Patient).Include(r => r.Receptionist).FirstOrDefaultAsync(i => i.Id == id);
            if (accountEntity == null) throw new Exception("Account not found");

            _context.Accounts.Remove(accountEntity);
            await _context.SaveChangesAsync();
        }
    }
}
