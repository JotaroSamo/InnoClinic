using BCrypt.Net;
using Auth_API.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth_API.Infrastructure
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Generate(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public bool Verify(string password, string hashedpassword)
         =>BCrypt.Net.BCrypt.EnhancedVerify(password, hashedpassword);
    }
}
