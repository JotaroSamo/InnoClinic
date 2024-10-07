using Appointment_API.DataAccess.Entity;
using Appointment_API.Domain.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.DataAccess.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Appointment, AppointmentEntity>().ReverseMap();
            CreateMap<DoctorAppointment, DoctorAppointmentEntity>().ReverseMap();
            CreateMap<PatientAppointment, PatientAppointmentEntity>().ReverseMap();
            CreateMap<ServiceAppointment, ServiceAppointmentEntity>().ReverseMap();
            CreateMap<Results, ResultEntity>().ReverseMap();

        }
    }
}
