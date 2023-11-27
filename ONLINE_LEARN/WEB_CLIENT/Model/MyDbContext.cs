using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() 
        { 

        }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<EnrollCourse> EnrollCourses { get; set; } = null!;
        public virtual DbSet<Lesson> Lessons { get; set; } = null!;
        public virtual DbSet<LessonPdf> LessonPdfs { get; set; } = null!;
        public virtual DbSet<LessonVideo> LessonVideos { get; set; } = null!;
        public virtual DbSet<Quiz> Quizzes { get; set; } = null!;
        public virtual DbSet<Result> Results { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                ConfigurationBuilder builder = new ConfigurationBuilder();
                IConfigurationRoot config = builder.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true).Build();
                string connection = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connection);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnrollCourse>().HasKey(e => new {e.CourseId, e.StudentId});
        }

    }
}
