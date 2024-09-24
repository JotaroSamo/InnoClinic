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
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public async Task<Doctor> CreateDoctorAsync(Doctor doctor)
        {
          return await _doctorRepository.CreateDoctorAsync(doctor);
        }

        public async Task DeleteDoctorAsync(Guid id)
        {
           await _doctorRepository.DeleteDoctorAsync(id);
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
          return await _doctorRepository.GetAllDoctorsAsync();
        }

        public async Task<Doctor> GetDoctorByIdAsync(Guid id)
        {
            return await _doctorRepository.GetDoctorByIdAsync(id);
        }

        public Task<Doctor> GetDoctorByNameAsync(string firstName, string lastName, string midleName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Doctor>> GetDoctorListBySpecializationAsync(Guid specId)
        {
            throw new NotImplementedException();
        }

        public async Task<Doctor> UpdateDoctorAsync(Guid id, Doctor doctor)
        {
            return await _doctorRepository.UpdateDoctorAsync(id, doctor);
        }
    }
}
