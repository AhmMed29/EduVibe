using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EduVibe.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;


namespace EduVibe.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public AppDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("connectionString");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public DbSet<InstructorCourse> InstructorCourses { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }

        [DbFunction("fn_GetStudentsAddress",schema: "dbo")]
        public IQueryable<Student> GetStudentsAddress(string city) 
            => FromExpression(() => GetStudentsAddress(city));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.HasDbFunction(
                typeof(AppDbContext)
                .GetMethod(nameof(GetStudentsAddress), new[] { typeof(string) })
                );

            modelBuilder.Entity<Course>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);
        }
    }
}
