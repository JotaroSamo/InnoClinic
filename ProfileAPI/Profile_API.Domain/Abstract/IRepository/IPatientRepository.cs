using Profile_API.Domain.Models;

namespace Profile_API.Domain.Abstract.IRepository
{
    public interface IPatientRepository
    {
        Task<Patient> CreatePatientAsync(Patient patient);
        Task DeletePatientAsync(int id);
        Task<List<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int id);
        Task<Patient> UpdatePatientAsync(int id, Patient patient);
    }
}