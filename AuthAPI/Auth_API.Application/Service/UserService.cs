using Auth_API.Domain.Abstract;
using Auth_API.Domain.Abstract.Repository;
using Auth_API.Domain.Abstract.Service;
using Auth_API.Domain.Entity;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Auth_API.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public Task<List<User>> GetUsers()
        {
           return _userRepository.GetUsers();
        }

        public async Task<Result<(string AccessToken, string RefreshToken)>> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return Result.Failure<(string AccessToken, string RefreshToken)>("User not found!");
            }
            if (!_passwordHasher.Verify(password, user.HashPassword))
            {
                return Result.Failure<(string AccessToken, string RefreshToken)>("Password not correct!");

            }
            var token = _jwtProvider.GenerateTokens(user);
            await _userRepository.RefreshToken(user.Id, token.RefreshToken);
            return Result.Success<(string AccessToken, string RefreshToken)>(token);
      
        }

        public async Task<Result<(string AccessToken, string RefreshToken)>> RefreshToken(string accessToken, string refreshToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                return Result.Failure<(string AccessToken, string RefreshToken)>("AccessToken not correct!");
            }
            string email = _jwtProvider.GetUserEmailFromExpiredToken(accessToken);

            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return Result.Failure<(string AccessToken, string RefreshToken)>("User not found!");
            }
            if (user.RefreshToken!=refreshToken)
            {
                return Result.Failure<(string AccessToken, string RefreshToken)>("RefreshToken not correct!");

            }
            var token = _jwtProvider.GenerateTokens(user);
            await _userRepository.RefreshToken(user.Id, token.RefreshToken);
            return Result.Success<(string AccessToken, string RefreshToken)>(token);
        }

        public async Task<Result<User>> RegisterAsync(string email, string password)
        {
            if (password.Length < 6 || password.Length > 16)
            {
                return Result.Failure<User>("Password length must be between 6 and 16 characters.");
            }
            var hashedpassword = _passwordHasher.Generate(password);
            var user = User.Create(Guid.NewGuid(),  email, hashedpassword);
            await _userRepository.AddAsync(user.Value);
            return user;
        }
    }
}
