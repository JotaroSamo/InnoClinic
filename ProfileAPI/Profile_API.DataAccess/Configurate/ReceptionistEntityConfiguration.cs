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
    public class ReceptionistEntityConfiguration : IEntityTypeConfiguration<ReceptionistEntity>
    {
        public void Configure(EntityTypeBuilder<ReceptionistEntity> builder)
        {
            // Указание ключа
            builder.HasKey(r => r.Id);

            // Конфигурация для имени
            builder.Property(r => r.FirstName)
                   .IsRequired()
                   .HasMaxLength(100); // ограничение на длину имени

            builder.Property(r => r.LastName)
                   .IsRequired()
                   .HasMaxLength(100); // ограничение на длину фамилии

            builder.Property(r => r.MiddleName)
                   .IsRequired()
                   .HasMaxLength(100); // ограничение на длину отчества

            builder.Property(r => r.OfficeAddress)
                   .HasMaxLength(255); // указать максимальную длину для адреса офиса

            // Конфигурация для телефона офиса
            builder.Property(r => r.OfficeRegistryPhoneNumber)
                   .HasMaxLength(20); // указать максимальную длину для номера телефона

            // Связь с AccountEntity
            builder.HasOne(r => r.Account)
                   .WithOne(a => a.Receptionist)
                   .HasForeignKey<ReceptionistEntity>(r => r.AccountId) // FK для Account
                   .IsRequired(); // обязательно наличие аккаунта
        }
    }
}
