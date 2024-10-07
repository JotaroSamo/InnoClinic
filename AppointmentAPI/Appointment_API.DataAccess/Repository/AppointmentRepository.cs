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
using Result = CSharpFunctionalExtensions.Result;

namespace Appointment_API.DataAccess.Repository
{


    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppointmentDbContext _context;
        private readonly IMapper _mapper;

        public AppointmentRepository(AppointmentDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Appointment>> Create(Appointment appointment)
        {
            var appointmentEntity = _mapper.Map<AppointmentEntity>(appointment);
            await _context.Appointments.AddAsync(appointmentEntity);
            try
            {
                await _context.SaveChangesAsync();
                return Result.Success(appointment);
            }
            catch (Exception ex)
            {
                return Result.Failure<Appointment>(ex.Message);
            }
        }

        public async Task<Result> Delete(Guid id)
        {
            var appointmentEntity = await _context.Appointments.FindAsync(id);
            if (appointmentEntity is null)
            {
                return Result.Failure("Appointment not found!");
            }

            await _context.Appointments.Where(a => a.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return Result.Success("Appointment deleted successfully.");
        }

        public async Task<Result<List<Appointment>>> GetAll()
        {
            var appointmentEntities = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Service)
                .ToListAsync();
            var appointments = _mapper.Map<List<Appointment>>(appointmentEntities);
            return Result.Success(appointments);
        }

        public async Task<Result<Appointment>> GetById(Guid id)
        {
            var appointmentEntity = await _context.Appointments.AsNoTracking()
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointmentEntity is null)
            {
                return Result.Failure<Appointment>("Appointment not found!");
            }

            var appointment = _mapper.Map<Appointment>(appointmentEntity);
            return Result.Success(appointment);
        }

        public async Task<Result<Appointment>> Update(Appointment appointment)
        {
            var appointmentEntity = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.Id == appointment.Id);

            if (appointmentEntity is null)
            {
                return Result.Failure<Appointment>("Appointment not found!");
            }

            _mapper.Map(appointment, appointmentEntity);
            _context.Appointments.Update(appointmentEntity);
            await _context.SaveChangesAsync();
            return Result.Success(appointment);
        }

        public async Task<Result<Appointment>> ChangeApprovalStatus(Guid id, bool isApproved)
        {
            var appointmentEntity = await _context.Appointments.FindAsync(id);
            if (appointmentEntity is null)
            {
                return Result.Failure<Appointment>("Appointment not found!");
            }

            appointmentEntity.IsApproved = isApproved;
            await _context.SaveChangesAsync();
            var appointment = _mapper.Map<Appointment>(appointmentEntity);
            return Result.Success(appointment);
        }
    }

}
