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

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Class> Class { get; set; }
    }
}
