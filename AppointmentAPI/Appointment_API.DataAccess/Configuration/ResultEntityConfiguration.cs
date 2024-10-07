using Appointment_API.DataAccess.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.DataAccess.Configuration
{
    public class ResultEntityConfiguration : IEntityTypeConfiguration<ResultEntity>
    {
        public void Configure(EntityTypeBuilder<ResultEntity> builder)
        {
            builder.ToTable("Results"); // Указание таблицы, если нужно

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Complaints)
                   .IsRequired()
                   .HasMaxLength(500); // Ограничение длины строки, если нужно

            builder.Property(r => r.Conclusion)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(r => r.Recommendations)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(r => r.AppointmentId)
                   .IsRequired();

            // Связь с AppointmentEntity
            builder.HasOne(r => r.Appointment)
                   .WithMany()
                   .HasForeignKey(r => r.AppointmentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
