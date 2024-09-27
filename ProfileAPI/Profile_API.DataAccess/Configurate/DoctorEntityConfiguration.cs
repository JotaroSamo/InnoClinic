using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Profile_API.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile_API.DataAccess.Configurate
{
    public class DoctorEntityConfiguration : IEntityTypeConfiguration<DoctorEntity>
    {
        public void Configure(EntityTypeBuilder<DoctorEntity> builder)
        {
            // Указание ключа
            builder.HasKey(d => d.Id);

            // Конфигурация для имени
            builder.Property(d => d.FirstName)
                   .IsRequired()
                   .HasMaxLength(100); // можно настроить длину имени

            builder.Property(d => d.LastName)
                   .IsRequired()
                   .HasMaxLength(100); // можно настроить длину фамилии

            builder.Property(d => d.MiddleName)
                   .IsRequired()
                   .HasMaxLength(100); // можно настроить длину отчества

            // Конфигурация для даты рождения
            builder.Property(d => d.DateOfBirth)
                   .IsRequired();

            // Конфигурация для CareerStartYear
            builder.Property(d => d.CareerStartYear)
                   .IsRequired();

            // Конфигурация для статуса
            builder.Property(d => d.Status)
                   .IsRequired();


            builder.Property(r => r.OfficeAddress)
               .HasMaxLength(255); // указать максимальную длину для адреса офиса

            // Конфигурация для телефона офиса
            builder.Property(r => r.OfficeRegistryPhoneNumber)
                   .HasMaxLength(20); // указать максимальную длину для номера телефона

            builder.HasOne(d => d.Specialization)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecializationId);
            // Конфигурация для AccountId
            builder.HasOne(d => d.Account)
                   .WithOne(a => a.Doctor)
                   .HasForeignKey<DoctorEntity>(d => d.AccountId) // внешний ключ
                   .IsRequired(); // обязательно наличие аккаунта
        }
    }
}
