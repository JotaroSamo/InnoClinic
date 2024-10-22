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
using StackExchange.Redis;
using System.Text.Json;

namespace Auth_API.Application.Service
{
   

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IConnectionMultiplexer _redis;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IConnectionMultiplexer redis)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _redis = redis; // Внедрение Redis
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userRepository.GetAll();
        }

        public async Task<Result<(string AccessToken, string RefreshToken)>> LoginAsync(string email, string password)
        {
            var db = _redis.GetDatabase();

            // Пытаемся получить пользователя из кэша
            var cachedUser = await db.StringGetAsync($"user:{email}");
            User user;
            if (!cachedUser.IsNullOrEmpty)
            {
                user = JsonSerializer.Deserialize<User>(cachedUser);
            }
            else
            {
                // Если пользователя нет в кэше, получаем из базы
                user = await _userRepository.GetByEmail(email);
                if (user == null)
                {
                    return Result.Failure<(string AccessToken, string RefreshToken)>("User not found!");
                }
                // Кэшируем пользователя в Redis
                await db.StringSetAsync($"user:{email}", JsonSerializer.Serialize(user), TimeSpan.FromMinutes(10));
            }

            // Проверка пароля
            if (!_passwordHasher.Verify(password, user.HashPassword))
            {
                return Result.Failure<(string AccessToken, string RefreshToken)>("Password not correct!");
            }

            var token = _jwtProvider.GenerateTokens(user);
            await _userRepository.RefreshToken(user.Id, token.RefreshToken);

            // Инвалидируем кэш для пользователя, если произошли изменения (например, RefreshToken)
            await db.StringSetAsync($"user:{email}", JsonSerializer.Serialize(user), TimeSpan.FromMinutes(10));

            return Result.Success<(string AccessToken, string RefreshToken)>(token);
        }

        public async Task<Result<(string AccessToken, string RefreshToken)>> RefreshToken(string accessToken, string refreshToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                return Result.Failure<(string AccessToken, string RefreshToken)>("AccessToken not correct!");
            }

            string email = _jwtProvider.GetUserEmailFromExpiredToken(accessToken);

            var db = _redis.GetDatabase();
            var cachedUser = await db.StringGetAsync($"user:{email}");
            User user;
            if (!cachedUser.IsNullOrEmpty)
            {
                user = JsonSerializer.Deserialize<User>(cachedUser);
            }
            else
            {
                user = await _userRepository.GetByEmail(email);
                if (user == null)
                {
                    return Result.Failure<(string AccessToken, string RefreshToken)>("User not found!");
                }
            }

            if (user.RefreshToken != refreshToken)
            {
                return Result.Failure<(string AccessToken, string RefreshToken)>("RefreshToken not correct!");
            }

            var token = _jwtProvider.GenerateTokens(user);
            await _userRepository.RefreshToken(user.Id, token.RefreshToken);

            // Обновляем кэш после изменения токенов
            await db.StringSetAsync($"user:{email}", JsonSerializer.Serialize(user), TimeSpan.FromMinutes(10));

            return Result.Success<(string AccessToken, string RefreshToken)>(token);
        }

        public async Task<Result<User>> RegisterAsync(string email, string password)
        {
            if (password.Length < 6 || password.Length > 16)
            {
                return Result.Failure<User>("Password length must be between 6 and 16 characters.");
            }

            var hashedPassword = _passwordHasher.Generate(password);
            var user = User.Create(Guid.NewGuid(), email, hashedPassword);

            await _userRepository.Create(user.Value);

            var db = _redis.GetDatabase();
            // Кэшируем нового пользователя
            await db.StringSetAsync($"user:{email}", JsonSerializer.Serialize(user.Value), TimeSpan.FromMinutes(10));

            return user;
        }
    }

}
