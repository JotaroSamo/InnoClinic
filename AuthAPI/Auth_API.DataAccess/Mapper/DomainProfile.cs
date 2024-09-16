using Auth_API.DataAccess.Entity;
using Auth_API.Domain.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth_API.DataAccess.Mapper
{
    public class DomainProfile: Profile
    {
        public DomainProfile()
        {
            CreateMap<User, UserEntity>().ReverseMap();
        }
    }
}
