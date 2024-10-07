using Appointment_API.DataAccess.Repository;
using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.Application.Service
{
    public class ServiceAppointmentService 
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceAppointmentService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<Result<ServiceAppointment>> CreateService(ServiceAppointment service)
        {
            var result = await _serviceRepository.Create(service);
            return result;
        }

        public async Task<Result> DeleteService(Guid id)
        {
            var result = await _serviceRepository.Delete(id);
            return result;
        }

        public async Task<Result<List<ServiceAppointment>>> GetAllServices()
        {
            var result = await _serviceRepository.GetAll();
            return result;
        }

        public async Task<Result<ServiceAppointment>> GetServiceById(Guid id)
        {
            var result = await _serviceRepository.GetById(id);
            return result;
        }

        public async Task<Result<ServiceAppointment>> UpdateService(ServiceAppointment service)
        {
            var result = await _serviceRepository.Update(service);
            return result;
        }
    }

}
