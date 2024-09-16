using Office_API.Domain.Enums;
using Office_API.Domain.Model;

namespace Office_API.Domain.Abstract.Repositories
{
    public interface IOfficeRepositories
    {
        Task<Office> Add(Office office);
        Task<Office> ChangeStatus(Guid id, Status isActive);
        Task<bool> Deletete(Guid id);
        Task<List<Office>> GetAll();
        Task<Office> GetById(Guid id);
        Task<Office> Update(Office office);
    }
}