using AutoMapper;
using Profile_API.DataAccess.Entity;
using Profile_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile_API.DataAccess.Mapper
{
   public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Account, AccountEntity>().ReverseMap();
            CreateMap<Doctor, DoctorEntity>().ReverseMap();
            CreateMap<Patient, PatientEntity>().ReverseMap();
            CreateMap<Receptionist, ReceptionistEntity>().ReverseMap();
            CreateMap<Specialization, SpecializationEntity>().ReverseMap();
        }
    }
}
