using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Profile_API.DataAccess.Entity;


using Profile_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile_API.DataAccess.Repositories
{
    public class ReceptionistRepository : IReceptionistRepository
    {
        private readonly ProfileDbContext _context;
        private readonly IMapper _mapper;

        public ReceptionistRepository(ProfileDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<Receptionist>>> GetAllReceptionistsAsync()
        {
            var receptionistsEntities = await _context.Receptionists.Include(r => r.Account).ToListAsync();
            var receptionists = _mapper.Map<List<Receptionist>>(receptionistsEntities);
            return Result.Success(receptionists);
        }

        public async Task<Result<Receptionist>> GetReceptionistByIdAsync(Guid id)
        {
            var receptionistEntity = await _context.Receptionists.Include(r => r.Account)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (receptionistEntity == null)
                return Result.Failure<Receptionist>("Receptionist not found");

            var receptionist = _mapper.Map<Receptionist>(receptionistEntity);
            return Result.Success(receptionist);
        }

        public async Task<Result<Receptionist>> CreateReceptionistAsync(Receptionist receptionist)
        {
            var receptionistEntity = _mapper.Map<ReceptionistEntity>(receptionist);
            _context.Receptionists.Add(receptionistEntity);
            await _context.SaveChangesAsync();

            var createdReceptionist = _mapper.Map<Receptionist>(receptionistEntity);
            return Result.Success(createdReceptionist);
        }

        public async Task<Result<Receptionist>> UpdateReceptionistAsync(Guid id, Receptionist receptionist)
        {
            var receptionistEntity = await _context.Receptionists.FindAsync(id);

            if (receptionistEntity == null)
                return Result.Failure<Receptionist>("Receptionist not found");

            _mapper.Map(receptionist, receptionistEntity);
            _context.Receptionists.Update(receptionistEntity);
            await _context.SaveChangesAsync();

            var updatedReceptionist = _mapper.Map<Receptionist>(receptionistEntity);
            return Result.Success(updatedReceptionist);
        }

        public async Task<Result> DeleteReceptionistAsync(Guid id)
        {
            var receptionistEntity = await _context.Receptionists.FindAsync(id);

            if (receptionistEntity == null)
                return Result.Failure("Receptionist not found");

            _context.Receptionists.Remove(receptionistEntity);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }

}
