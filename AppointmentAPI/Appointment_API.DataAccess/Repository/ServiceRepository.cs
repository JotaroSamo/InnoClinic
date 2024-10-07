using Appointment_API.DataAccess.Entity;
using Appointment_API.Domain.Model;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.DataAccess.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppointmentDbContext _context;
        private readonly IMapper _mapper;

        public ServiceRepository(AppointmentDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ServiceAppointment>> Create(ServiceAppointment service)
        {
            var serviceEntity = _mapper.Map<ServiceAppointmentEntity>(service);
            await _context.Services.AddAsync(serviceEntity);
            try
            {
                await _context.SaveChangesAsync();
                return Result.Success(service);
            }
            catch (Exception ex)
            {
                return Result.Failure<ServiceAppointment>(ex.Message);
            }
        }

        public async Task<Result> Delete(Guid id)
        {
            var serviceEntity = await _context.Services.FindAsync(id);
            if (serviceEntity is null)
            {
                return Result.Failure("Service not found!");
            }

            _context.Services.Remove(serviceEntity);
            await _context.SaveChangesAsync();
            return Result.Success("Service deleted successfully.");
        }


        public async Task<Result<ServiceAppointment>> Update(ServiceAppointment service)
        {
            var serviceEntity = await _context.Services
                .Include(s => s.Appointments)
                .FirstOrDefaultAsync(s => s.Id == service.Id);

            if (serviceEntity is null)
            {
                return Result.Failure<ServiceAppointment>("Service not found!");
            }

            _mapper.Map(service, serviceEntity);
            _context.Services.Update(serviceEntity);
            await _context.SaveChangesAsync();
            return Result.Success(service);
        }
    }
}
