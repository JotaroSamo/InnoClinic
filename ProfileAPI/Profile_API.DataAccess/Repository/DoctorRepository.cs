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
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ProfileDbContext _context;
        private readonly IMapper _mapper;

        public DoctorRepository(ProfileDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<Doctor>>> GetAll()
        {
            var doctorsEntities = await _context.Doctors
                .Include(d => d.Account)
                .Include(s => s.Specialization).AsNoTracking()
                .ToListAsync();

            var doctors = _mapper.Map<List<Doctor>>(doctorsEntities);
            return Result.Success(doctors);
        }

        public async Task<Result<Doctor>> GetById(Guid id)
        {
            var doctorEntity = await _context.Doctors
                .Include(d => d.Account)
                .Include(s => s.Specialization).AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (doctorEntity == null)
                return Result.Failure<Doctor>("Doctor not found");

            var doctor = _mapper.Map<Doctor>(doctorEntity);
            return Result.Success(doctor);
        }

        public async Task<Result<Doctor>> Create(Doctor doctor)
        {
            var doctorEntity = _mapper.Map<DoctorEntity>(doctor);
            _context.Doctors.Add(doctorEntity);
            await _context.SaveChangesAsync();

            var createdDoctor = _mapper.Map<Doctor>(doctorEntity);
            return Result.Success(createdDoctor);
        }

        public async Task<Result<Doctor>> GetByName(string firstName, string lastName, string midleName)
        {
            var doctorEntity = await _context.Doctors
                .FirstOrDefaultAsync(n => n.FirstName == firstName &&
                                          n.LastName == lastName &&
                                          n.MiddleName == midleName);

            if (doctorEntity == null)
                return Result.Failure<Doctor>("Doctor not found");

            var doctor = _mapper.Map<Doctor>(doctorEntity);
            return Result.Success(doctor);
        }

        public async Task<Result<List<Doctor>>> GetBySpecialization(Guid specId)
        {
            var doctorEntities = await _context.Doctors
                .Where(i => i.SpecializationId == specId)
                .ToListAsync();

            var doctors = _mapper.Map<List<Doctor>>(doctorEntities);
            return Result.Success(doctors);
        }

        public async Task<Result<Doctor>> Update(Guid id, Doctor doctor)
        {
            var doctorEntity = await _context.Doctors.FindAsync(id);

            if (doctorEntity == null)
                return Result.Failure<Doctor>("Doctor not found");

            _mapper.Map(doctor, doctorEntity);
            _context.Doctors.Update(doctorEntity);
            await _context.SaveChangesAsync();

            var updatedDoctor = _mapper.Map<Doctor>(doctorEntity);
            return Result.Success(updatedDoctor);
        }

        public async Task<Result> Delete(Guid id)
        {
            var doctorEntity = await _context.Doctors.FindAsync(id);

            if (doctorEntity == null)
                return Result.Failure("Doctor not found");

            _context.Doctors.Remove(doctorEntity);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }

}
