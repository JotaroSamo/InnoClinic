using Appointment_API.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.DataAccess
{
    public class AppointmentDbContext : DbContext
    {
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options)
         : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentDbContext).Assembly);
        }
        public DbSet<AppointmentEntity> Appointments { get; set; }
        public DbSet<ResultEntity> Results { get; set; }

        public DbSet<DoctorAppointmentEntity> Doctors { get; set; }

        public DbSet<PatientAppointmentEntity> Patients { get; set; }

        public DbSet<ServiceAppointmentEntity> Services { get; set; }

    }
}
