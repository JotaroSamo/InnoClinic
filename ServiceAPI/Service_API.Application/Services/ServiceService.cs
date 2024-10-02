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
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<Result<Service>> ChangeStatusService(Guid id, bool status)
        {
            var result = await _serviceRepository.ChangeStatus(id, status);
            return result;
        }

        public async Task<Result<Service>> CreateService(Service service)
        {
            var result = await _serviceRepository.Create(service);
            return result;
        }

        public async Task<Result> Delete(Guid id)
        {
            var result = await _serviceRepository.Delete(id);
            return result;
        }

        public async Task<Result<List<Service>>> GetAllService()
        {
            var result = await _serviceRepository.GetAll(); ;
            return result;
        }

        public async Task<Result<Service>> GetByIdService(Guid id)
        {
            var result = await _serviceRepository.GetById(id);
            return result;
        }

        public async Task<Result<Service>> UpdateService(Service service)
        {
            var result = await _serviceRepository.Update(service);
            return result;
        }
    }
}
