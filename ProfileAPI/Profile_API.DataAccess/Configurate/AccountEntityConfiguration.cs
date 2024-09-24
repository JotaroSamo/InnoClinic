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
    public class AccountEntityConfiguration : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            // Указание ключа
            builder.HasKey(a => a.Id);

            // Конфигурация для Email
            builder.Property(a => a.Email)
                   .IsRequired()
                   .HasMaxLength(255);

            // Конфигурация для пароля
            builder.Property(a => a.Password)
                   .IsRequired()
                   .HasMaxLength(255); // можно настроить длину

            // Конфигурация для номера телефона
            builder.Property(a => a.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(20); // указать максимальную длину для номера телефона

            // Конфигурация для проверки почты
            builder.Property(a => a.IsEmailVerified)
                   .IsRequired();

            // Конфигурация аудиторских данных
            builder.Property(a => a.CreatedBy)
                   .HasMaxLength(100);
            builder.Property(a => a.UpdatedBy)
                   .HasMaxLength(100);

            // Время создания и обновления
            builder.Property(a => a.CreatedAt);
            builder.Property(a => a.UpdatedAt);

            // Конфигурация для фотографий
            builder.Property(a => a.PhotoId)
                   .HasMaxLength(100); // можно указать тип varchar
            builder.Property(a => a.PhotoUrl)
                   .HasMaxLength(255); // можно указать длину для URL

            // Конфигурация отношений между сущностями
            builder.HasOne(a => a.Receptionist)
                   .WithOne(r => r.Account)
                   .HasForeignKey<ReceptionistEntity>(r => r.AccountId); // FK для Receptionist

            builder.HasOne(a => a.Doctor)
                   .WithOne(d => d.Account)
                   .HasForeignKey<DoctorEntity>(d => d.AccountId); // FK для Doctor

            builder.HasOne(a => a.Patient)
                   .WithOne(p => p.Account)
                   .HasForeignKey<PatientEntity>(p => p.AccountId); // FK для Patient
        }
    }
}
