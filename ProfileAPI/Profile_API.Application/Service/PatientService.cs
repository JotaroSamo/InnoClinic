using Profile_API.Domain.Abstract.IRepository;
using Profile_API.Domain.Abstract.IService;
using Profile_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile_API.Application.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public async Task<Patient> CreatePatientAsync(Patient patient)
        {
           return await _patientRepository.CreatePatientAsync(patient);
        }

        public async Task DeletePatientAsync(int id)
        {
           await _patientRepository.DeletePatientAsync(id);
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _patientRepository.GetAllPatientsAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
          return await _patientRepository.GetPatientByIdAsync(id);
        }

        public async Task<Patient> UpdatePatientAsync(int id, Patient patient)
        {
           return await _patientRepository.UpdatePatientAsync(id, patient);
        }
    }
}
