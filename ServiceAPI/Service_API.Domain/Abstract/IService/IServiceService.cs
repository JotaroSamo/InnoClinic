using CSharpFunctionalExtensions;
using Service_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.Domain.Abstract.IService
{
    public interface IServiceService
    {
        public Task<Result<List<Service>>> GetAllService();
        public Task<Result<Service>> GetByIdService(Guid id);
        public Task<Result<Service>> CreateService(Service service);
        public Task<Result<Service>> UpdateService(Service service);
        public Task<Result> Delete(Guid id);
        public Task<Result<Service>> ChangeStatusService(Guid id, bool status);
    }
}
