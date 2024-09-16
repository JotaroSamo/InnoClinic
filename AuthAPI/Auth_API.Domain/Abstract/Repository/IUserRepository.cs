using Auth_API.Domain.Entity;

namespace Auth_API.Domain.Abstract.Repository
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User> GetByEmailAsync(string email);
        Task<List<User>> GetUsers();
        Task RefreshToken(Guid id, string refreshToken);
    }
}