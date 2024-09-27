using CSharpFunctionalExtensions;
using Service_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.Domain.Abstract.IRepository
{
    public interface IServiceCategoryRepository
    {
        Task<Result<ServiceCategory>> Create(ServiceCategory serviceCategory);
        Task<Result> Delete(Guid id);
        Task<Result<List<ServiceCategory>>> GetAll();
        Task<Result<ServiceCategory>> GetById(Guid id);
        Task<Result<ServiceCategory>> Update(ServiceCategory serviceCategory);
    }
}
