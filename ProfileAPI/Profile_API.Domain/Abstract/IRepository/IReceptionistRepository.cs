using Profile_API.Domain.Models;

namespace Profile_API.Domain.Abstract.IRepository
{
    public interface IReceptionistRepository
    {
        Task<Receptionist> CreateReceptionistAsync(Receptionist receptionist);
        Task DeleteReceptionistAsync(Guid id);
        Task<List<Receptionist>> GetAllReceptionistsAsync();
        Task<Receptionist> GetReceptionistByIdAsync(Guid id);
        Task<Receptionist> UpdateReceptionistAsync(Guid id, Receptionist receptionist);
    }
}