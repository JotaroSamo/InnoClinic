using Auth_API.Domain.Entity;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth_API.Domain.Abstract.Service
{
    public interface IUserService
    {
        Task<Result<User>> RegisterAsync(string email, string password);
        Task<Result<(string AccessToken, string RefreshToken)>> LoginAsync(string email, string password);
        Task<Result<(string AccessToken, string RefreshToken)>> RefreshToken(string accessToken, string refreshToken);

        Task<List<User>> GetUsers();

   

    }
}
