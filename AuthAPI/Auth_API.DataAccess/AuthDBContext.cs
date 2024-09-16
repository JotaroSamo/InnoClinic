using Auth_API.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth_API.DataAccess
{
    public class AuthDBContext : DbContext
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> options)
         : base(options)
        {
        }
        public  DbSet<UserEntity> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDBContext).Assembly);
        }
    }
}
