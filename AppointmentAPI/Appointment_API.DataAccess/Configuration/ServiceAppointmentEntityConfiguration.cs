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
    public class ServiceAppointmentEntityConfiguration : IEntityTypeConfiguration<ServiceAppointmentEntity>
    {
        public void Configure(EntityTypeBuilder<ServiceAppointmentEntity> builder)
        {
            // Установка ключа
            builder.HasKey(s => s.Id);

            // Установка ограничений на поля
            builder.Property(s => s.Service_Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.Service_Price)
                   .IsRequired()
                   .HasPrecision(10, 2); // Настройка точности для хранения цены

            // Связь один-ко-многим с AppointmentEntity
            builder.HasMany(s => s.Appointments)
                   .WithOne(a => a.Service)
                   .HasForeignKey(a => a.ServiceId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Настройка имени таблицы
            builder.ToTable("Services");
        }
    }
}
