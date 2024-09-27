using CSharpFunctionalExtensions;
using Service_API.Domain.Abstract.IRepository;
using Service_API.Domain.Abstract.IService;
using Service_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.Application.Services
{
    public class ServiceCategoryService : IServiceCategoryService
    {
        private readonly IServiceCategoryRepository _serviceCategoryRepository;

        public ServiceCategoryService(IServiceCategoryRepository serviceCategoryRepository)
        {
            _serviceCategoryRepository = serviceCategoryRepository;
        }

        public async Task<Result<ServiceCategory>> CreateCategoryService(ServiceCategory serviceCategory)
        {
            var result = await _serviceCategoryRepository.Create(serviceCategory);
            return result;
        }

        public async Task<Result> DeleteCategoryService(Guid id)
        {
            var result = await _serviceCategoryRepository.Delete(id);
            return result;
        }

        public async Task<Result<List<ServiceCategory>>> GetAllCategoryService()
        {
            var result = await _serviceCategoryRepository.GetAll();
            return result;
        }

        public async Task<Result<ServiceCategory>> GetByIdCategoryService(Guid id)
        {
            var result = await _serviceCategoryRepository.GetById(id);
            return result;
        }

        public async Task<Result<ServiceCategory>> UpdateCategoryService(ServiceCategory serviceCategory)
        {
            var result = await _serviceCategoryRepository.Update(serviceCategory);
            return result;
        }
    }
}
