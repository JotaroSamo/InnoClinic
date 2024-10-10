using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Appointment_API.DataAccess.Entity;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;

namespace Appointment_API.DataAccess.Repository
{

    public class ResultRepository : IResultRepository
    {
        private readonly AppointmentDbContext _context;
        private readonly IMapper _mapper;

        public ResultRepository(AppointmentDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Results>> Create(Results result)
        {
            var resultEntity = _mapper.Map<ResultEntity>(result);
            await _context.Results.AddAsync(resultEntity);
            try
            {
                await _context.SaveChangesAsync();
                return Result.Success(result);
            }
            catch (Exception ex)
            {
                return Result.Failure<Results>(ex.Message);
            }
        }

        public async Task<Result> Delete(Guid id)
        {
            var resultEntity = await _context.Results.FindAsync(id);
            if (resultEntity is null)
            {
                return Result.Failure("Result not found!");
            }

            _context.Results.Remove(resultEntity);
            await _context.SaveChangesAsync();
            return Result.Success("Result deleted successfully.");
        }

        public async Task<Result<List<Results>>> GetAll()
        {
            var resultEntities = await _context.Results
                .Include(r => r.Appointment)
                .ToListAsync();
            var results = _mapper.Map<List<Results>>(resultEntities);
            return Result.Success(results);
        }

        public async Task<Result<Results>> GetById(Guid id)
        {
            var resultEntity = await _context.Results.AsNoTracking()
                .Include(r => r.Appointment).ThenInclude(p=>p.Patient)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (resultEntity is null)
            {
                return Result.Failure<Results>("Result not found!");
            }

            var result = _mapper.Map<Results>(resultEntity);
            return Result.Success(result);
        }

        public async Task<Result<Results>> Update(Results result)
        {
            var resultEntity = await _context.Results
                .Include(r => r.Appointment)
                .FirstOrDefaultAsync(r => r.Id == result.Id);

            if (resultEntity is null)
            {
                return Result.Failure<Results>("Result not found!");
            }

            _mapper.Map(result, resultEntity);
            _context.Results.Update(resultEntity);
            await _context.SaveChangesAsync();
            return Result.Success(result);
        }
    }

}
