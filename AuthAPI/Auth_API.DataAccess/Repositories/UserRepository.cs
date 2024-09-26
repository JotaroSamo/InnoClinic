using Auth_API.DataAccess.Entity;
using Auth_API.Domain.Abstract.Repository;
using Auth_API.Domain.Entity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth_API.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDBContext _dbContext;

        private readonly IMapper _mapper;

        public UserRepository(AuthDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        // Поиск по почте
        public async Task<User> GetByEmailAsync(string email)
        {
            var userEntity = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            return _mapper.Map<User>(userEntity);
        }

        // Добавление пользователя
        public async Task AddAsync(User user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);
            await _dbContext.Users.AddAsync(userEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            var userEntity = await _dbContext.Users.AsNoTracking().ToListAsync();
            return _mapper.Map<List<User>>(userEntity);
        }

        public async Task RefreshToken(Guid id, string refreshToken)
        {
            var userEntity = await _dbContext.Users.FindAsync(id);
            userEntity.RefreshToken = refreshToken;
            await _dbContext.SaveChangesAsync(); 
        }

    }
}
