using Auth_API.Domain.Entity;

namespace Auth_API.Domain.Abstract.Repository
{
    public interface IUserRepository
    {
        Task Create(User user);
        Task<User> GetByEmail(string email);
        Task<List<User>> GetAll();
        Task RefreshToken(Guid id, string refreshToken);
    }
}