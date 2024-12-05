using Microsoft.EntityFrameworkCore;
using _12591_API.Modals;
using Microsoft.Extensions.Hosting;
using _12591_API.Models;

namespace _12591_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<FutureGoal> FutureGoals { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasIndex(u => u.Username)
               .IsUnique();

            modelBuilder.Entity<User>()
              .HasIndex(u => u.Email)
              .IsUnique();

            modelBuilder.Entity<User>()
                           .HasIndex(u => u.Username)
                           .IsUnique();
        }
    }
}
