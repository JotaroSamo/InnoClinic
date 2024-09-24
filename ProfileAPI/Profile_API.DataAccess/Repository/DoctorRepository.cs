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
   public class DoctorRepository : IDoctorRepository
    {
        private readonly ProfileDbContext _context;
        private readonly IMapper _mapper;

        public DoctorRepository(ProfileDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
            var doctors = await _context.Doctors.Include(d => d.Account)
                .Include(s=>s.Specialization)
                .ToListAsync();
            return _mapper.Map<List<Doctor>>(doctors);
        }

        public async Task<Doctor> GetDoctorByIdAsync(Guid id)
        {
            var doctor = await _context.Doctors.Include(d => d.Account)
                .Include(s => s.Specialization).
                FirstOrDefaultAsync(d => d.Id == id);
            if (doctor == null) throw new Exception("Doctor not found");
            return _mapper.Map<Doctor>(doctor);
        }

        public async Task<Doctor> CreateDoctorAsync(Doctor doctor)
        {
            var doctorEntity = _mapper.Map<DoctorEntity>(doctor);
            _context.Doctors.Add(doctorEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<Doctor>(doctorEntity);
        }

        public async Task<Doctor> GetDoctorByNameAsync(string firstName, string lastName, string midleName)
        {
            var doctorEntity = await _context.Doctors.FirstOrDefaultAsync(n =>
                n.FirstName == firstName &&
                    n.LastName == lastName && n.MiddleName == midleName);
            return _mapper.Map<Doctor>(doctorEntity);
        }

        public async Task<List<Doctor>> GetDoctorListBySpecializationAsync(Guid specId)
        {
            var doctorEntity = await _context.Doctors.Where(i => i.SpecializationId == specId).ToListAsync();
            return _mapper.Map<List<Doctor>>(doctorEntity);
        }

        public async Task<Doctor> UpdateDoctorAsync(Guid id, Doctor doctor)
        {
            var doctorEntity = await _context.Doctors.FindAsync(id);
            if (doctorEntity == null) throw new Exception("Doctor not found");

            _mapper.Map(doctor, doctorEntity);
            _context.Doctors.Update(doctorEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<Doctor>(doctorEntity);
        }

        public async Task DeleteDoctorAsync(Guid id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) throw new Exception("Doctor not found");

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        
        }
    }
}
