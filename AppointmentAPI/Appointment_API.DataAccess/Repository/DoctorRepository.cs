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
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppointmentDbContext _context;
        private readonly IMapper _mapper;

        public DoctorRepository(AppointmentDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<DoctorAppointment>> Create(DoctorAppointment doctor)
        {
            var doctorEntity = _mapper.Map<DoctorAppointmentEntity>(doctor);
            await _context.Doctors.AddAsync(doctorEntity);
            try
            {
                await _context.SaveChangesAsync();
                return Result.Success(doctor);
            }
            catch (Exception ex)
            {
                return Result.Failure<DoctorAppointment>(ex.Message);
            }
        }

        public async Task<Result> Delete(Guid id)
        {
            var doctorEntity = await _context.Doctors.FindAsync(id);
            if (doctorEntity is null)
            {
                return Result.Failure("Doctor not found!");
            }

            _context.Doctors.Remove(doctorEntity);
            await _context.SaveChangesAsync();
            return Result.Success("Doctor deleted successfully.");
        }


        public async Task<Result<DoctorAppointment>> Update(DoctorAppointment doctor)
        {
            var doctorEntity = await _context.Doctors
                .Include(d => d.Appointments)
                .FirstOrDefaultAsync(d => d.Id == doctor.Id);

            if (doctorEntity is null)
            {
                return Result.Failure<DoctorAppointment>("Doctor not found!");
            }

            _mapper.Map(doctor, doctorEntity);
            _context.Doctors.Update(doctorEntity);
            await _context.SaveChangesAsync();
            return Result.Success(doctor);
        }
    }
}
