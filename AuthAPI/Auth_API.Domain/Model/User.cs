using Auth_API.Domain.Enums;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth_API.Domain.Entity
{
    public  class User
    {
        public Guid Id { get; set; }
 
        public string Email { get; set; } = string.Empty;
       
        public string HashPassword { get; set; } = string.Empty;
        public Role Role { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public User() 
        {
        }
        private User(Guid guid, string email, string hashpassword, string refreshToken, Role role)
        {
            Id = guid;
            Email = email;
            HashPassword = hashpassword;
            Role = role;
            RefreshToken = refreshToken;
        }
        public static Result<User> Create(Guid id, string email, string hashPassword)
        {
            if (string.IsNullOrEmpty(hashPassword) )
            {
                return Result.Failure<User>($"The {nameof(hashPassword)} is null");
            }
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                return Result.Failure<User>($"The {nameof(email)} is null or this not email");
            }

            var user = new User(id, email, hashPassword, string.Empty, Role.Patients);
            return Result.Success(user);
        }

    }

}
