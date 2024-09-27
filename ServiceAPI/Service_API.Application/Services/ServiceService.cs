using CSharpFunctionalExtensions;
using Service_API.Domain.Abstract.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.Application.Service
{
    public class ServiceService : IServiceService
    {
        public Task<Result<Domain.Model.Service>> ChangeStatusService(Guid id, bool status)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Domain.Model.Service>> CreateService(Domain.Model.Service service)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteService(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<Domain.Model.Service>>> GetAllService()
        {
            throw new NotImplementedException();
        }

        public Task<Result<Domain.Model.Service>> GetByIdService(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Domain.Model.Service>> UpdateService(Domain.Model.Service service)
        {
            throw new NotImplementedException();
        }
    }
}
