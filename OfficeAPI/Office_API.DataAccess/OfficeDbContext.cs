using Microsoft.EntityFrameworkCore;
using Office_API.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office_API.DataAccess
{
    public class OfficeDbContext : DbContext
    {
        public OfficeDbContext(DbContextOptions<OfficeDbContext> options)
         : base(options)
        {
        }
        public DbSet<OfficeEntity> Offices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OfficeDbContext).Assembly);
        }
    }
}
