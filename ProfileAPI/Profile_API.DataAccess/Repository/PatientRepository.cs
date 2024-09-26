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
    public class PatientRepository : IPatientRepository
    {
        private readonly ProfileDbContext _context;
        private readonly IMapper _mapper;

        public PatientRepository(ProfileDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<Patient>>> GetAllPatientsAsync()
        {
            var patientsEntities = await _context.Patients.Include(p => p.Account).AsNoTracking().ToListAsync();
            var patients = _mapper.Map<List<Patient>>(patientsEntities);
            return Result.Success(patients);
        }

        public async Task<Result<Patient>> GetPatientByIdAsync(Guid id)
        {
            var patientEntity = await _context.Patients.Include(p => p.Account).AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patientEntity == null)
                return Result.Failure<Patient>("Patient not found");

            var patient = _mapper.Map<Patient>(patientEntity);
            return Result.Success(patient);
        }

        public async Task<Result<Patient>> CreatePatientAsync(Patient patient)
        {
            var patientEntity = _mapper.Map<PatientEntity>(patient);
            _context.Patients.Add(patientEntity);
            await _context.SaveChangesAsync();

            var createdPatient = _mapper.Map<Patient>(patientEntity);
            return Result.Success(createdPatient);
        }

        public async Task<Result<Patient>> UpdatePatientAsync(Guid id, Patient patient)
        {
            var patientEntity = await _context.Patients.FindAsync(id);

            if (patientEntity == null)
                return Result.Failure<Patient>("Patient not found");

            _mapper.Map(patient, patientEntity);
            _context.Patients.Update(patientEntity);
            await _context.SaveChangesAsync();

            var updatedPatient = _mapper.Map<Patient>(patientEntity);
            return Result.Success(updatedPatient);
        }

        public async Task<Result> DeletePatientAsync(Guid id)
        {
            var patientEntity = await _context.Patients.FindAsync(id);

            if (patientEntity == null)
                return Result.Failure("Patient not found");

            _context.Patients.Remove(patientEntity);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }


}
