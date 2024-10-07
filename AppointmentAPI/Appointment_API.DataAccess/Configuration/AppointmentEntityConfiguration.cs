using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment_API.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Appointment_API.DataAccess.Configurate
{
   

    public class AppointmentEntityConfiguration : IEntityTypeConfiguration<AppointmentEntity>
    {
        public void Configure(EntityTypeBuilder<AppointmentEntity> builder)
        {
            builder.ToTable("Appointments"); // Указание таблицы, если нужно

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Date)
                   .IsRequired();

            builder.Property(a => a.Time)
                   .IsRequired();

            builder.Property(a => a.IsApproved)
                   .IsRequired();

            builder.Property(a => a.PatientId)
                   .IsRequired();

            builder.Property(a => a.DoctorId)
                   .IsRequired();

            builder.Property(a => a.ServiceId)
                   .IsRequired();

            // Связь с PatientAppointmentEntity
            builder.HasOne(a => a.Patient)
                   .WithMany()
                   .HasForeignKey(a => a.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Связь с DoctorAppointmentEntity
            builder.HasOne(a => a.Doctor)
                   .WithMany()
                   .HasForeignKey(a => a.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Связь с ServiceAppointmentEntity
            builder.HasOne(a => a.Service)
                   .WithMany()
                   .HasForeignKey(a => a.ServiceId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
