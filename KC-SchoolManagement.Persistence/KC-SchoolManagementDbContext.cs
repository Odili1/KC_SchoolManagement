using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KC_SchoolManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace KC_SchoolManagement.Persistence
{
    public class KC_SchoolManagementDbContext : DbContext
    {
        public KC_SchoolManagementDbContext(DbContextOptions<KC_SchoolManagementDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        DbSet<Student> Students { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        DbSet<Subject> Subjects { get; set; }
        DbSet<Class> Class { get; set; }
    }
}
