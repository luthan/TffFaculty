using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication2.Models;

namespace WebApplication2.Models
{
    public class TffFacultyContext : IdentityDbContext<User>
    {
        //public TffFacultyContext()
        //{
        //    //Database.EnsureCreated();
        //}

        public DbSet<Event> Events { get; set; }
        public DbSet<EventFaculty> EventFaculty { get; set; }
        public DbSet<FacultyRole> FacultyRoles { get; set; }
        public DbSet<Disclosure> Disclosures { get; set; }
        public DbSet<EventFacultyRole> EventFacultyRoles { get; set; }
        public DbSet<EntityFrameworkLog> EntityFrameworkLog { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<StatusCode> StatusCodes { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
////#if DEBUG
////            var connString = Startup.Configuration["Data:ConnectionString"];
////#else
////            var connString = Startup.Configuration["Data:TffFacultyConnection"];
////#endif
////            optionsBuilder.UseSqlServer(connString);
//            base.OnConfiguring(optionsBuilder);
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventFaculty>()
                .HasKey(m => new { m.UserId, m.EventId });

            modelBuilder.Entity<EventFaculty>()
                .HasOne(m => m.User)
                .WithMany(m => m.EventFaculty)
                .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<EventFaculty>()
                .HasOne(m => m.Event)
                .WithMany(m => m.EventUsers)
                .HasForeignKey(m => m.EventId);

            //modelBuilder.Entity<User>()
            //    .HasMany(m => m.Disclosures)
            //    .WithOne(m => m.User)
            //    .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<EventFacultyRole>()
                .HasKey(m => new { m.UserId, m.EventId, m.FacultyRoleId });

            modelBuilder.Entity<Specialty>()
                .HasKey(m => new { m.Id });

            modelBuilder.Entity<EventFacultyRole>()
                .HasOne(m => m.User)
                .WithMany(m => m.EventFacultyRoles)
                .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<EventFacultyRole>()
                .HasOne(m => m.FacultyRole)
                .WithMany(m => m.EventFacultyRoles)
                .HasForeignKey(m => m.FacultyRoleId);

            modelBuilder.Entity<EventFacultyRole>()
                .HasOne(m => m.Event)
                .WithMany(m => m.EventFacultyRoles)
                .HasForeignKey(m => m.EventId);
        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
////#if DEBUG
////            var connString = Startup.Configuration["Data:ConnectionString"];
////#else
////            var connString = Startup.Configuration["Data:TffFacultyConnection"];
////#endif
////            optionsBuilder.UseSqlServer(connString);
//            base.OnConfiguring(optionsBuilder);
//        }

        public DbSet<User> User { get; set; }
    }
}
