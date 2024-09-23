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
    public class PatientEntityConfiguration : IEntityTypeConfiguration<PatientEntity>
    {
        public void Configure(EntityTypeBuilder<PatientEntity> builder)
        {
            // Указание ключа
            builder.HasKey(p => p.Id);

            // Конфигурация для имени
            builder.Property(p => p.FirstName)
                   .IsRequired()
                   .HasMaxLength(100); // можно настроить длину имени

            builder.Property(p => p.LastName)
                   .IsRequired()
                   .HasMaxLength(100); // можно настроить длину фамилии

            builder.Property(p => p.MiddleName)
                   .IsRequired()
                   .HasMaxLength(100); // можно настроить длину отчества

            // Конфигурация для IsLinkedToAccount
            builder.Property(p => p.IsLinkedToAccount)
                   .IsRequired();

            // Конфигурация для даты рождения
            builder.Property(p => p.DateOfBirth)
                   .IsRequired();

            // Конфигурация для AccountId и связь с AccountEntity
            builder.HasOne(p => p.Account)
                   .WithOne(a => a.Patient)
                   .HasForeignKey<PatientEntity>(p => p.AccountId) // FK для Account
                   .IsRequired(); // обязательно наличие аккаунта
        }
    }
}
