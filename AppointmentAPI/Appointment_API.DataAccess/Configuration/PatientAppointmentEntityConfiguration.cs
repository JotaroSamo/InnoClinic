using Appointment_API.DataAccess.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.DataAccess.Configuration
{
    public class PatientAppointmentEntityConfiguration : IEntityTypeConfiguration<PatientAppointmentEntity>
    {
        public void Configure(EntityTypeBuilder<PatientAppointmentEntity> builder)
        {
            // Установка ключа
            builder.HasKey(p => p.Id);

            // Установка ограничений на поля
            builder.Property(p => p.Patient_Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Number_Phone)
                   .IsRequired()
                   .HasMaxLength(15);

            // Связь один-ко-многим с AppointmentEntity
            builder.HasMany(p => p.Appointments)
                   .WithOne(a => a.Patient)
                   .HasForeignKey(a => a.PatientId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Настройка имени таблицы
            builder.ToTable("Patients");
        }
    }
}
