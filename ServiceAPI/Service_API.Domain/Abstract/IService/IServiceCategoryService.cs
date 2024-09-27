using CSharpFunctionalExtensions;
using Service_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.Domain.Abstract.IService
{
    public interface IServiceCategoryService
    {
        Task<Result<ServiceCategory>> CreateCategoryService(ServiceCategory serviceCategory);
        Task<Result> DeleteCategoryService(Guid id);
        Task<Result<List<ServiceCategory>>> GetAllCategoryService();
        Task<Result<ServiceCategory>> GetByIdCategoryService(Guid id);
        Task<Result<ServiceCategory>> UpdateCategoryService(ServiceCategory serviceCategory);
    }
}
