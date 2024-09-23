using Microsoft.EntityFrameworkCore;
using Profile_API.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile_API.DataAccess
{
    public class ProfileDbContext:DbContext
    {
        public ProfileDbContext(DbContextOptions<ProfileDbContext> options)
         : base(options)
        {
        }
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<DoctorEntity> Doctors { get; set; }
        public DbSet<PatientEntity> Patients { get; set; }
        public DbSet<ReceptionistEntity> Receptionists { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProfileDbContext).Assembly);
        }
    }
}
