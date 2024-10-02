using AutoMapper;
using Service_API.DataAccess.Entity;
using Service_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.DataAccess.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Service, ServiceEntity>().ReverseMap();
            CreateMap<ServiceCategory, ServiceCategoryEntity>().ReverseMap();
            CreateMap<Specialization, SpecializationEntity>().ReverseMap();
        }
    }
}
