using Profile_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile_API.Domain.Abstract.IService
{
    public interface IDoctorService
    {
        Task<List<Doctor>> GetAllDoctorsAsync();
        Task<Doctor> GetDoctorByIdAsync(Guid id);
        Task<List<Doctor>> GetDoctorListBySpecializationAsync(Guid specId);
        Task<Doctor> GetDoctorByNameAsync(string firstName, string lastName, string midleName);
        Task<Doctor> CreateDoctorAsync(Doctor doctor);
        Task<Doctor> UpdateDoctorAsync(Guid id, Doctor doctor);
        Task DeleteDoctorAsync(Guid id);
    }
}
