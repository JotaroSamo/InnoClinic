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
    public class PatientRepository : IPatientRepository
    {
        private readonly ProfileDbContext _context;
        private readonly IMapper _mapper;

        public PatientRepository(ProfileDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients.Include(p => p.Account).ToListAsync();
            return _mapper.Map<List<Patient>>(patients);
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients.Include(p => p.Account).FirstOrDefaultAsync(p => p.Id == id);
            if (patient == null) throw new Exception("Patient not found");
            return _mapper.Map<Patient>(patient);
        }

        public async Task<Patient> CreatePatientAsync(Patient patient)
        {
            var patientEntity = _mapper.Map<PatientEntity>(patient);
            _context.Patients.Add(patientEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<Patient>(patientEntity);
        }

        public async Task<Patient> UpdatePatientAsync(int id, Patient patient)
        {
            var patientEntity = await _context.Patients.FindAsync(id);
            if (patientEntity == null) throw new Exception("Patient not found");

            _mapper.Map(patient, patientEntity);
            _context.Patients.Update(patientEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<Patient>(patientEntity);
        }

        public async Task DeletePatientAsync(int id)
        {
            var patientEntity = await _context.Patients.FindAsync(id);
            if (patientEntity == null) throw new Exception("Patient not found");

            _context.Patients.Remove(patientEntity);
            await _context.SaveChangesAsync();
        }
    }

}
