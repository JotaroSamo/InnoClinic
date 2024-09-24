using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Profile_API.DataAccess.Entity;
using Profile_API.Domain.Abstract.IRepository;
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
    public async Task<List<Specialization>> GetAllSpecializationsAsync()
    {
        var spec = await _context.Specializations
            .Include(d=>d.Doctors).ToListAsync();

        return _mapper.Map<List<Specialization>>(spec);

    }

    public async Task<Specialization> GetSpecializationByIdAsync(Guid id)
    {
        var spec = await _context.Specializations
            .Include(d=>d.Doctors).FirstOrDefaultAsync(i => i.Id == id);
        return _mapper.Map<Specialization>(spec);
    }

    public async Task<Specialization> CreateSpecializationAsync(Specialization specialization)
    {
        var spec = _mapper.Map<SpecializationEntity>(specialization);
        await _context.Specializations.AddAsync(spec);
        await _context.SaveChangesAsync();
        return specialization;
    }

    public async Task<Specialization> UpdateSpecializationAsync(Guid id, Specialization specialization)
    {
        var spec = await _context.Specializations.FindAsync(id);
        if (spec == null) throw new Exception("Specialization not found");

        _mapper.Map(specialization, spec);
        _context.Specializations.Update(spec);
        await _context.SaveChangesAsync();

        return _mapper.Map<Specialization>(spec);
    }

    public async Task DeleteSpecializationAsync(Guid id)
    {
        var spec = await _context.Specializations.FindAsync(id);
        _context.Remove(spec);
        await _context.SaveChangesAsync();  
    }
}