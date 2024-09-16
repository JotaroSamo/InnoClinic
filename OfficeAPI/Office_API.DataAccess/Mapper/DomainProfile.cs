using AutoMapper;
using Office_API.Domain.Model;
using Office_API.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office_API.DataAccess.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Office, OfficeEntity>().ReverseMap();
        }
    }
}
