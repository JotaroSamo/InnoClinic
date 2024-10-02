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
    public class ServiceCategoryRepository : IServiceCategoryRepository
    {
        private readonly ServiceDBContext _context;
        private readonly IMapper _mapper;

        public ServiceCategoryRepository(ServiceDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ServiceCategory>> Create(ServiceCategory serviceCategory)
        {
            var serviceCategoryEntity = _mapper.Map<ServiceCategoryEntity>(serviceCategory);
            await _context.ServiceCategories.AddAsync(serviceCategoryEntity);
            try
            {
                await _context.SaveChangesAsync();
                return Result.Success(serviceCategory);
            }
            catch (Exception ex)
            {
                return Result.Failure<ServiceCategory>(ex.Message);
            }
        }

        public async Task<Result> Delete(Guid id)
        {
            var serviceCategoryEntity = await _context.ServiceCategories.FindAsync(id);
            if (serviceCategoryEntity is null)
            {
                return Result.Failure("Not found service category!");
            }
            await _context.ServiceCategories.Where(sc => sc.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return Result.Success("Deleted successfully");
        }

        public async Task<Result<List<ServiceCategory>>> GetAll()
        {
            var serviceCategories = await _context.ServiceCategories
                .Include(sc => sc.Services) // Подгружаем связанные сервисы
                .ToListAsync();
            var mappedCategories = _mapper.Map<List<ServiceCategory>>(serviceCategories);
            return Result.Success(mappedCategories);
        }

        public async Task<Result<ServiceCategory>> GetById(Guid id)
        {
            var serviceCategoryEntity = await _context.ServiceCategories.AsNoTracking()
                .Include(sc => sc.Services)
                .FirstOrDefaultAsync(sc => sc.Id == id);
            if (serviceCategoryEntity is null)
            {
                return Result.Failure<ServiceCategory>("Service category not found!");
            }
            var serviceCategory = _mapper.Map<ServiceCategory>(serviceCategoryEntity);
            return Result.Success(serviceCategory);
        }

        public async Task<Result<ServiceCategory>> Update(ServiceCategory serviceCategory)
        {
            var serviceCategoryEntity = await _context.ServiceCategories.Include(sc => sc.Services)
                .FirstOrDefaultAsync(sc => sc.Id == serviceCategory.Id);
            if (serviceCategoryEntity is  null)
            {
                return Result.Failure<ServiceCategory>("Service category not found!");
            }
            _mapper.Map(serviceCategory, serviceCategoryEntity);
            _context.ServiceCategories.Update(serviceCategoryEntity);
            await _context.SaveChangesAsync();
            return Result.Success(serviceCategory);
        }

    }

}
