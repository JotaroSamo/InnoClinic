using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Service_API.DataAccess.Entity;
using Service_API.Domain.Abstract.IRepository;
using Service_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.DataAccess.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ServiceDBContext _context;
        private readonly IMapper _mapper;

        public ServiceRepository(ServiceDBContext serviceDBContext, IMapper mapper)
        {
            _context = serviceDBContext;
            _mapper = mapper;
        }

        public async Task<Result<Service>> ChangeStatus(Guid id, bool status)
        {
            var serviceEntity = await _context.Services.FindAsync(id);
            if (serviceEntity is null)
            {
                return Result.Failure<Service>("Not found service!");
            }
            serviceEntity.IsActive = status;
            await _context.SaveChangesAsync();
            var service = _mapper.Map<Service>(serviceEntity);
            return Result.Success(service);
            }

        public async Task<Result<Service>> Create(Service service)
        {
            var serviceEntity = _mapper.Map<ServiceEntity>(service);
            await _context.Services.AddAsync(serviceEntity);
            try
            {
                await _context.SaveChangesAsync();
                return Result.Success(service);
            }
            catch (Exception ex)
            {
                return Result.Failure<Service>(ex.Message);
            }
    
        }

        public async Task<Result> Delete(Guid id)
        {
            var serviceEntity = await _context.Services.FindAsync(id);
            if (serviceEntity is null)
            {
                return Result.Failure("Not found service!");
            }
            await _context.Services.Where(i=>i.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return Result.Success("Ok");
        }

        public async Task<Result<List<Service>>> GetAll()
        {
            var serviceEntity = await _context.Services
                .Include(s=>s.Specialization)
                .Include(c=>c.ServiceCategory)
                .ToListAsync();
            var service = _mapper.Map<List<Service>>(serviceEntity);
            return Result.Success(service);
        }

        public async Task<Result<Service>> GetById(Guid id)
        {
            var serviceEntity = await _context.Services.AsNoTracking()
               .Include(s => s.Specialization)
               .Include(c => c.ServiceCategory)
               .FirstOrDefaultAsync(i=>i.Id == id);
            if (serviceEntity is null)
            {
                return Result.Failure<Service>("Service not found!");
            }
            var service = _mapper.Map<Service>(serviceEntity);
            return Result.Success(service);

        }

        public async Task<Result<Service>> Update(Service service)
        {
            var serviceEntity = await _context.Services.
                Include(s => s.Specialization)
               .Include(c => c.ServiceCategory)
               .FirstOrDefaultAsync(i => i.Id == service.Id);
            _mapper.Map(service, serviceEntity);
            _context.Services.Update(serviceEntity);
            await _context.SaveChangesAsync();
            return Result.Success(service); 
        }
    }
}
