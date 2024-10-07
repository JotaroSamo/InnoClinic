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
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Result<Appointment>> ChangeApprovalStatus(Guid id, bool isApproved)
        {
            var result = await _appointmentRepository.ChangeApprovalStatus(id, isApproved);
            return result;
        }

        public async Task<Result<Appointment>> CreateAppointment(Appointment appointment)
        {
            var result = await _appointmentRepository.Create(appointment);
            return result;
        }

        public async Task<Result> Delete(Guid id)
        {
            var result = await _appointmentRepository.Delete(id);
            return result;
        }

        public async Task<Result<List<Appointment>>> GetAllAppointments()
        {
            var result = await _appointmentRepository.GetAll();
            return result;
        }

        public async Task<Result<Appointment>> GetByIdAppointment(Guid id)
        {
            var result = await _appointmentRepository.GetById(id);
            return result;
        }

        public async Task<Result<Appointment>> UpdateAppointment(Appointment appointment)
        {
            var result = await _appointmentRepository.Update(appointment);
            return result;
        }
    }

}
