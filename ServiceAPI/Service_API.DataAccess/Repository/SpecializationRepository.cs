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
    public class SpecializationRepository : ISpecializationRepository
    {
        private readonly ServiceDBContext _context;
        private readonly IMapper _mapper;

        public SpecializationRepository(ServiceDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Specialization>> Create(Specialization specialization)
        {
            var specializationEntity = _mapper.Map<SpecializationEntity>(specialization);
            await _context.Specializations.AddAsync(specializationEntity);
            try
            {
                await _context.SaveChangesAsync();
                return Result.Success(specialization);
            }
            catch (Exception ex)
            {
                return Result.Failure<Specialization>(ex.Message);
            }
        }

        public async Task<Result> Delete(Guid id)
        {
            var specializationEntity = await _context.Specializations.FindAsync(id);
            if (specializationEntity == null)
            {
                return Result.Failure("Specialization not found!");
            }
            await _context.Specializations.Where(sp => sp.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return Result.Success("Deleted successfully");
        }

        public async Task<Result<List<Specialization>>> GetAll()
        {
            var specializations = await _context.Specializations
                .Include(sp => sp.Services) // Подгружаем связанные сервисы
                .ToListAsync();
            var mappedSpecializations = _mapper.Map<List<Specialization>>(specializations);
            return Result.Success(mappedSpecializations);
        }

        public async Task<Result<Specialization>> GetById(Guid id)
        {
            var specializationEntity = await _context.Specializations.AsNoTracking()
                .Include(sp => sp.Services)
                .FirstOrDefaultAsync(sp => sp.Id == id);
            if (specializationEntity == null)
            {
                return Result.Failure<Specialization>("Specialization not found!");
            }
            var specialization = _mapper.Map<Specialization>(specializationEntity);
            return Result.Success(specialization);
        }

        public async Task<Result<Specialization>> Update(Specialization specialization)
        {
            var specializationEntity = await _context.Specializations.Include(sp => sp.Services)
                .FirstOrDefaultAsync(sp => sp.Id ==specialization.Id); ;
            if (specializationEntity == null)
            {
                return Result.Failure<Specialization>("Specialization not found!");
            }
            _mapper.Map(specialization, specializationEntity);
            _context.Specializations.Update(specializationEntity);
            await _context.SaveChangesAsync();
            return Result.Success(specialization);
        }
    }
}
