using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service_API.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.DataAccess.Configuration
{
    public class SpecializationConfiguration : IEntityTypeConfiguration<SpecializationEntity>
    {
        public void Configure(EntityTypeBuilder<SpecializationEntity> builder)
        {
            // Настройка первичного ключа
            builder.HasKey(sp => sp.Id);

            // Уникальное ограничение на имя специализации
            builder.HasIndex(sp => sp.SpecializationName).IsUnique();

            // Обязательные поля
            builder.Property(sp => sp.SpecializationName)
                   .IsRequired()
                   .HasMaxLength(150); // Задайте необходимую длину строки

            builder.Property(sp => sp.IsActive)
                   .IsRequired();

            // Связь с таблицей ServiceEntity
            builder.HasMany(sp => sp.Services)
                   .WithOne(s => s.Specialization)
                   .HasForeignKey(s => s.SpecializationId)
                   .OnDelete(DeleteBehavior.Restrict); // При удалении специализации сервисы остаются
        }
    }
}
