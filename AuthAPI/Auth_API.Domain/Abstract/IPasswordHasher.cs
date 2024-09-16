using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth_API.Domain.Abstract
{
    public interface IPasswordHasher
    {
        string Generate(string password);
        bool Verify(string password, string hashedpassword);
    }
}
