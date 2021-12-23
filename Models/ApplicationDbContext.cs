using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace FPTAppDev.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Staff> StaffDbset { get; set; }
        public DbSet<Trainer> TrainerDbset { get; set; }
        public DbSet<Trainee> TraineeDbset { get; set; }
        public DbSet<Category> CategoryDbset { get; set; }
        public DbSet<Course> CourseDbset { get; set; }
        public DbSet<TrainerCourse> TrainerCourseDbset { get; set; }
        public DbSet<TraineeCourse> TraineeCourseDbset { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}