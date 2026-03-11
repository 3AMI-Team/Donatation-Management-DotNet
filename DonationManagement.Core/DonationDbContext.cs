using Microsoft.EntityFrameworkCore;
using DonationManagement.Core.Entities;

namespace DonationManagement.Core
{
    public class DonationDbContext : DbContext
    {
        public DonationDbContext(DbContextOptions<DonationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Distribution> Distributions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<Case>()
                .HasOne(c => c.Supervisor)
                .WithMany(e => e.RegisteredCases)
                .HasForeignKey(c => c.SupervisorId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Case>()
                .HasOne(c => c.Donor)
                .WithMany(d => d.Cases)
                .HasForeignKey(c => c.DonorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Case>()
                .HasOne(c => c.Category)
                .WithMany(cat => cat.Cases)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Distribution>()
                .HasOne(d => d.Case)
                .WithMany(c => c.Distributions)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Distribution>()
                .HasOne(d => d.HandledByEmployee)
                .WithMany(e => e.DistributionsHandled)
                .HasForeignKey(d => d.HandledByEmployeeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
