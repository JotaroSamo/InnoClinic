using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Profile_API.DataAccess.Entity;
using Profile_API.Domain.Abstract.IRepository;

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

        public async Task<List<Receptionist>> GetAllReceptionistsAsync()
        {
            var receptionists = await _context.Receptionists.Include(r => r.Account).ToListAsync();
            return _mapper.Map<List<Receptionist>>(receptionists);
        }

        public async Task<Receptionist> GetReceptionistByIdAsync(Guid id)
        {
            var receptionist = await _context.Receptionists.Include(r => r.Account).FirstOrDefaultAsync(r => r.Id == id);
            if (receptionist == null) throw new Exception("Receptionist not found");
            return _mapper.Map<Receptionist>(receptionist);
        }

        public async Task<Receptionist> CreateReceptionistAsync(Receptionist receptionist)
        {
            var receptionistEntity = _mapper.Map<ReceptionistEntity>(receptionist);
            _context.Receptionists.Add(receptionistEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<Receptionist>(receptionistEntity);
        }

        public async Task<Receptionist> UpdateReceptionistAsync(Guid id, Receptionist receptionist)
        {
            var receptionistEntity = await _context.Receptionists.FindAsync(id);
            if (receptionistEntity == null) throw new Exception("Receptionist not found");

            _mapper.Map(receptionist, receptionistEntity);
            _context.Receptionists.Update(receptionistEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<Receptionist>(receptionistEntity);
        }

        public async Task DeleteReceptionistAsync(Guid id)
        {
            var receptionistEntity = await _context.Receptionists.FindAsync(id);
            if (receptionistEntity == null) throw new Exception("Receptionist not found");

            _context.Receptionists.Remove(receptionistEntity);
            await _context.SaveChangesAsync();
        }
    }
}
