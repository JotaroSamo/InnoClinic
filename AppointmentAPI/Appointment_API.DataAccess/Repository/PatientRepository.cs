using Appointment_API.DataAccess.Entity;
using Appointment_API.Domain.Model;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.DataAccess.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppointmentDbContext _context;
        private readonly IMapper _mapper;

        public PatientRepository(AppointmentDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PatientAppointment>> Create(PatientAppointment patient)
        {
            var patientEntity = _mapper.Map<PatientAppointmentEntity>(patient);
            await _context.Patients.AddAsync(patientEntity);
            try
            {
                await _context.SaveChangesAsync();
                return Result.Success(patient);
            }
            catch (Exception ex)
            {
                return Result.Failure<PatientAppointment>(ex.Message);
            }
        }

        public async Task<Result> Delete(Guid id)
        {
            var patientEntity = await _context.Patients.FindAsync(id);
            if (patientEntity is null)
            {
                return Result.Failure("Patient not found!");
            }

            _context.Patients.Remove(patientEntity);
            await _context.SaveChangesAsync();
            return Result.Success("Patient deleted successfully.");
        }

        public async Task<Result<List<PatientAppointment>>> GetAll()
        {
            var patientEntities = await _context.Patients
                .Include(p => p.Appointments)
                .ToListAsync();
            var patients = _mapper.Map<List<PatientAppointment>>(patientEntities);
            return Result.Success(patients);
        }

        public async Task<Result<PatientAppointment>> GetById(Guid id)
        {
            var patientEntity = await _context.Patients.AsNoTracking()
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patientEntity is null)
            {
                return Result.Failure<PatientAppointment>("Patient not found!");
            }

            var patient = _mapper.Map<PatientAppointment>(patientEntity);
            return Result.Success(patient);
        }

        public async Task<Result<PatientAppointment>> Update(PatientAppointment patient)
        {
            var patientEntity = await _context.Patients
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(p => p.Id == patient.Id);

            if (patientEntity is null)
            {
                return Result.Failure<PatientAppointment>("Patient not found!");
            }

            _mapper.Map(patient, patientEntity);
            _context.Patients.Update(patientEntity);
            await _context.SaveChangesAsync();
            return Result.Success(patient);
        }
    }
}
