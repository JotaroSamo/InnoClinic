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
    public class SpecializationEntityConfiguration : IEntityTypeConfiguration<SpecializationEntity>
    {
        public void Configure(EntityTypeBuilder<SpecializationEntity> builder)
        {
 

            // Устанавливаем первичный ключ
            builder.HasKey(s => s.Id);

            // Указываем обязательность поля
            builder.Property(s => s.SpecializationName)
                   .IsRequired()
                   .HasMaxLength(200);

            // Уникальность поля SpecializationName
            builder.HasIndex(s => s.SpecializationName)
                   .IsUnique(); 

          

        }
    }
}
