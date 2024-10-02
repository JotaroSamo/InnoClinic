using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Service_API.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.DataAccess.Configuration
{
    public class ServiceConfiguration : IEntityTypeConfiguration<ServiceEntity>
    {
        public void Configure(EntityTypeBuilder<ServiceEntity> builder)
        {
            // Настройка первичного ключа
            builder.HasKey(s => s.Id);

            // Обязательные поля
            builder.Property(s => s.ServiceName)
                   .IsRequired()
                   .HasMaxLength(200); // Задайте необходимую длину строки

            builder.Property(s => s.Price)
                   .IsRequired();

            builder.Property(s => s.IsActive)
                   .IsRequired();

            // Связь с таблицей ServiceCategoryEntity
            builder.HasOne(s => s.ServiceCategory)
                   .WithMany(sc => sc.Services)
                   .HasForeignKey(s => s.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade); // При удалении категории удаляются связанные сервисы

            // Связь с таблицей SpecializationEntity (один ко многим)
            builder.HasOne(s => s.Specialization)
                   .WithMany(sp => sp.Services)
                   .HasForeignKey(s => s.SpecializationId)
                   .OnDelete(DeleteBehavior.Restrict); // При удалении специализации сервисы остаются
        }
    }
}
