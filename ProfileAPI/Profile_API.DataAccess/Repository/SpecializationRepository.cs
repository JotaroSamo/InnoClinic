using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Profile_API.DataAccess.Entity;
using Profile_API.Domain.Models;

namespace Profile_API.DataAccess.Repositories;

public class SpecializationRepository : ISpecializationRepository
{
    private readonly ProfileDbContext _context;
    private readonly IMapper _mapper;

    public SpecializationRepository(ProfileDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<Specialization>>> GetAllSpecializationsAsync()
    {
        var specializationsEntities = await _context.Specializations
            .Include(d => d.Doctors).AsNoTracking()
            .ToListAsync();

        var specializations = _mapper.Map<List<Specialization>>(specializationsEntities);
        return Result.Success(specializations);
    }

    public async Task<Result<Specialization>> GetSpecializationByIdAsync(Guid id)
    {
        var specializationEntity = await _context.Specializations
            .Include(d => d.Doctors).AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);

        if (specializationEntity == null)
            return Result.Failure<Specialization>("Specialization not found");

        var specialization = _mapper.Map<Specialization>(specializationEntity);
        return Result.Success(specialization);
    }

    public async Task<Result<Specialization>> CreateSpecializationAsync(Specialization specialization)
    {
        var specializationEntity = _mapper.Map<SpecializationEntity>(specialization);
        await _context.Specializations.AddAsync(specializationEntity);
        await _context.SaveChangesAsync();

        var createdSpecialization = _mapper.Map<Specialization>(specializationEntity);
        return Result.Success(createdSpecialization);
    }

    public async Task<Result<Specialization>> UpdateSpecializationAsync(Guid id, Specialization specialization)
    {
        var specializationEntity = await _context.Specializations.FindAsync(id);
        if (specializationEntity == null)
            return Result.Failure<Specialization>("Specialization not found");

        _mapper.Map(specialization, specializationEntity);
        _context.Specializations.Update(specializationEntity);
        await _context.SaveChangesAsync();

        var updatedSpecialization = _mapper.Map<Specialization>(specializationEntity);
        return Result.Success(updatedSpecialization);
    }

    public async Task<Result> DeleteSpecializationAsync(Guid id)
    {
        var specializationEntity = await _context.Specializations.FindAsync(id);
        if (specializationEntity == null)
            return Result.Failure("Specialization not found");

        _context.Specializations.Remove(specializationEntity);
        await _context.SaveChangesAsync();

        return Result.Success();
    }
}
