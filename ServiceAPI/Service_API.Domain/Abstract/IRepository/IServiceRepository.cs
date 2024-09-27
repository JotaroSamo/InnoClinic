using CSharpFunctionalExtensions;
using Service_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.Domain.Abstract.IRepository
{
    public interface IServiceRepository
    {
        public Task<Result<List<Service>>> GetAll();
        public Task<Result<Service>> GetById(Guid id);
        public Task<Result<Service>> Create(Service service);
        public Task<Result<Service>> Update(Service service);
        public Task<Result> Delete(Guid id);
        public Task<Result<Service>> ChangeStatus(Guid id, bool status);
    }
}
