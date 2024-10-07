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
    public class DoctorAppointmentEntityConfiguration : IEntityTypeConfiguration<DoctorAppointmentEntity>
    {
        public void Configure(EntityTypeBuilder<DoctorAppointmentEntity> builder)
        {
            // Установка ключа
            builder.HasKey(d => d.Id);

            // Установка ограничений на поля
            builder.Property(d => d.Doctro_Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(d => d.Specialization_Name)
                   .IsRequired()
                   .HasMaxLength(100);

            // Связь один-ко-многим с AppointmentEntity
            builder.HasMany(d => d.Appointments)
                   .WithOne(a => a.Doctor)
                   .HasForeignKey(a => a.DoctorId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Настройка имени таблицы
            builder.ToTable("Doctors");
        }
    }
}
