using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Office_API.DataAccess.Entity;

namespace Office_API.DataAccess.Configurate
{
  
    public class OfficeEntityConfiguration : IEntityTypeConfiguration<OfficeEntity>
    {
        public void Configure(EntityTypeBuilder<OfficeEntity> builder)
        {
            // Установка имени таблицы в базе данных
            builder.ToTable("Offices");

            // Настройка первичного ключа
            builder.HasKey(o => o.Id);

            // Настройка обязательных полей
            builder.Property(o => o.City)
                .IsRequired()
                .HasMaxLength(100); // Пример длины

            builder.Property(o => o.Street)
                .IsRequired()
                .HasMaxLength(100); // Пример длины

            builder.Property(o => o.HouseNumber)
                .IsRequired()
                .HasMaxLength(50); // Пример длины

            builder.Property(o => o.OfficeNumber)
                .IsRequired()
                .HasMaxLength(50); // Пример длины

            builder.Property(o => o.RegistryPhoneNumber)
                .IsRequired()
                .HasMaxLength(20); // Пример длины

            // Настройка необязательных полей
            builder.Property(o => o.PhotoId)
                .IsRequired(false);

            builder.Property(o => o.PhotoUrl)
                .HasMaxLength(500); // Пример длины

            // Настройка поля статуса
            builder.Property(o => o.IsActive)
                .IsRequired();

        }
    }

}
