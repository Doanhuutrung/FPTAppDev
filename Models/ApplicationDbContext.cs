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
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}