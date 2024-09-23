using Profile_API.Domain.Models;

namespace Profile_API.Domain.Abstract.IService
{
    public interface IReceptionistService
    {
        Task<Receptionist> CreateReceptionistAsync(Receptionist receptionist);
        Task DeleteReceptionistAsync(Guid id);
        Task<List<Receptionist>> GetAllReceptionistsAsync();
        Task<Receptionist> GetReceptionistByIdAsync(Guid id);
        Task<Receptionist> UpdateReceptionistAsync(Guid id, Receptionist receptionist);
    }
}