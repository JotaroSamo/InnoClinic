using Appointment_API.DataAccess.Repository;
using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.Application.Service
{
    public class DoctorAppointmentService : IDoctorAppointmentService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorAppointmentService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<Result<DoctorAppointment>> CreateDoctor(DoctorAppointment doctor)
        {
            var result = await _doctorRepository.Create(doctor);
            return result;
        }

        public async Task<Result> DeleteDoctor(Guid id)
        {
            var result = await _doctorRepository.Delete(id);
            return result;
        }

        public async Task<Result<List<DoctorAppointment>>> GetAllDoctors()
        {
            var result = await _doctorRepository.GetAll();
            return result;
        }

        public async Task<Result<DoctorAppointment>> GetDoctorById(Guid id)
        {
            var result = await _doctorRepository.GetById(id);
            return result;
        }

        public async Task<Result<DoctorAppointment>> UpdateDoctor(DoctorAppointment doctor)
        {
            var result = await _doctorRepository.Update(doctor);
            return result;
        }
    }

}
