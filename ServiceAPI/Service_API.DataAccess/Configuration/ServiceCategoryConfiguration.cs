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
    public class ServiceCategoryConfiguration : IEntityTypeConfiguration<ServiceCategoryEntity>
    {
        public void Configure(EntityTypeBuilder<ServiceCategoryEntity> builder)
        {
            // Настройка первичного ключа
            builder.HasKey(sc => sc.Id);

            // Уникальное ограничение на имя категории
            builder.HasIndex(sc => sc.CategoryName).IsUnique();

            // Обязательные поля
            builder.Property(sc => sc.CategoryName)
                   .IsRequired()
                   .HasMaxLength(100); // Задайте необходимую длину строки

            builder.Property(sc => sc.TimeSlotSize)
                   .IsRequired();

            // Связь с таблицей ServiceEntity (один ко многим)
            builder.HasMany(sc => sc.Services)
                   .WithOne(s => s.ServiceCategory)
                   .HasForeignKey(s => s.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
