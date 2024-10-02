using Microsoft.EntityFrameworkCore;
using Service_API.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.DataAccess
{
    public class ServiceDBContext : DbContext
    {
        public ServiceDBContext(DbContextOptions<ServiceDBContext> options)
         : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServiceDBContext).Assembly);
        }
        public DbSet<ServiceCategoryEntity> ServiceCategories { get; set; }
        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<SpecializationEntity> Specializations { get; set; }
    }
}
