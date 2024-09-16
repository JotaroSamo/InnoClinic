using Auth_API.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth_API.DataAccess.Configurate
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            // Установка первичного ключа
            builder.HasKey(u => u.Id);

            // Добавление уникального индекса для поля Email
            builder.HasIndex(u => u.Email)
        .IsUnique();

            // Конфигурация Email
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256); 

            // Конфигурация HashPassword
            builder.Property(u => u.HashPassword)
                .IsRequired()
                .HasMaxLength(256); 

            // Конфигурация Role
            builder.Property(u => u.Role)
                .IsRequired();
        }
    }

}
