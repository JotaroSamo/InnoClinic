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
    public class PatientAppointmentService : IPatientAppointmentService
    {
        private readonly IPatientRepository _patienRepository;

        public PatientAppointmentService(IPatientRepository patientRepository)
        {
            _patienRepository = patientRepository;
        }

        public async Task<Result<PatientAppointment>> CreatePatient(PatientAppointment patient)
        {
            var result = await _patienRepository.Create(patient);
            return result;
        }

        public async Task<Result> DeletePatient(Guid id)
        {
            var result = await _patienRepository.Delete(id);
            return result;
        }

        public async Task<Result<List<PatientAppointment>>> GetAllPatients()
        {
            var result = await _patienRepository.GetAll();
            return result;
        }

        public async Task<Result<PatientAppointment>> GetPatientById(Guid id)
        {
            var result = await _patienRepository.GetById(id);
            return result;
        }

        public async Task<Result<PatientAppointment>> UpdatePatient(PatientAppointment patient)
        {
            var result = await _patienRepository.Update(patient);
            return result;
        }
    }

}
