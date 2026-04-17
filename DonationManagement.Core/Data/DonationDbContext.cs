using Microsoft.EntityFrameworkCore;
using DonationManagement.Core.Entities;
using System;

namespace DonationManagement.Core.Data
{
    public class DonationDbContext : DbContext
    {
        public DonationDbContext(DbContextOptions<DonationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Distribution> Distributions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Donation relationships
            modelBuilder.Entity<Donation>()
                .HasOne(d => d.Supervisor)
                .WithMany(e => e.DonationsRegistered)
                .HasForeignKey(d => d.SupervisorId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Donation>()
                .HasOne(d => d.Donor)
                .WithMany(donor => donor.Donations)
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Donation>()
                .HasOne(d => d.Category)
                .WithMany(cat => cat.Donations)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Case relationships
            modelBuilder.Entity<Case>()
                .HasOne(c => c.Supervisor)
                .WithMany(e => e.CasesRegistered)
                .HasForeignKey(c => c.SupervisorId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Case>()
                .HasOne(c => c.Category)
                .WithMany(cat => cat.Cases)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Distribution relationships - using Restrict to avoid cascade cycles
            modelBuilder.Entity<Distribution>()
                .HasOne(dist => dist.Case)
                .WithMany(c => c.Distributions)
                .HasForeignKey(dist => dist.CaseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Distribution>()
                .HasOne(dist => dist.Donation)
                .WithMany(don => don.Distributions)
                .HasForeignKey(dist => dist.DonationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Distribution>()
                .HasOne(dist => dist.HandledByEmployee)
                .WithMany(e => e.DistributionsHandled)
                .HasForeignKey(dist => dist.HandledByEmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure decimal precision
            modelBuilder.Entity<Donation>()
                .Property(d => d.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Distribution>()
                .Property(d => d.Amount)
                .HasPrecision(18, 2);

            // Seed Data (English Only)
            var sharedPasswordHash = "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W"; // 12345678

            // Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "Ibrahim Nasser", Username = "Ibrahim", Password = sharedPasswordHash, Role = "Admin", Phone = "01278988474", Address = "Ismailia, Egypt", Email = "12baraka34@gmail.com" },
                new Employee { Id = 2, Name = "Sarah Connor", Username = "SarahC", Password = sharedPasswordHash, Role = "Supervisor", Phone = "+12025550199", Address = "New York, USA", Email = "sarah.c@example.com" },
                new Employee { Id = 3, Name = "John Doe", Username = "JohnD", Password = sharedPasswordHash, Role = "FieldWorker", Phone = "+12025550188", Address = "London, UK", Email = "john.doe@example.com" }
            );

            // Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 101, Type = "Education", Description = "Student sponsorship and school supplies" },
                new Category { Id = 102, Type = "Healthcare", Description = "Medications, surgeries, and medical equipment" },
                new Category { Id = 103, Type = "Food Security", Description = "Food packages and meal distributions" },
                new Category { Id = 104, Type = "Housing", Description = "Home renovation and clean water access" },
                new Category { Id = 105, Type = "Emergency Relief", Description = "Disaster response and urgent assistance" }
            );

            // Donors
            modelBuilder.Entity<Donor>().HasData(
                new Donor { Id = 101, Name = "James Wilson", Email = "james.w@example.com", Password = sharedPasswordHash, Phone = "+12025550101", Address = "Los Angeles, USA", Type = "Individual", RegisterDate = new DateTime(2025, 1, 10, 10, 0, 0, DateTimeKind.Utc) },
                new Donor { Id = 102, Name = "Mary Johnson", Email = "mary.j@example.com", Password = sharedPasswordHash, Phone = "+12025550102", Address = "Chicago, USA", Type = "Individual", RegisterDate = new DateTime(2025, 2, 5, 12, 30, 0, DateTimeKind.Utc) },
                new Donor { Id = 103, Name = "Global Tech Corp", Email = "donations@globaltech.com", Password = sharedPasswordHash, Phone = "+12025550300", Address = "San Francisco, USA", Type = "Corporate", RegisterDate = new DateTime(2025, 3, 12, 9, 15, 0, DateTimeKind.Utc) }
            );

            // Cases (Beneficiaries)
            modelBuilder.Entity<Case>().HasData(
                new Case { Id = 201, Name = "Alice Peterson", Phone = "+12025550501", Address = "Detroit, USA", RegistDate = new DateTime(2026, 1, 5, 0, 0, 0, DateTimeKind.Utc), Status = "Approved", Description = "Needs a new wheelchair for educational mobility", CategoryId = 102, SupervisorId = 2 },
                new Case { Id = 202, Name = "Robert's Family", Phone = "+12025550502", Address = "Houston, USA", RegistDate = new DateTime(2026, 1, 15, 0, 0, 0, DateTimeKind.Utc), Status = "Pending Review", Description = "Monthly food support for a family of 6", CategoryId = 103, SupervisorId = 2 },
                new Case { Id = 203, Name = "St. Paul Orphanage", Phone = "+12025550503", Address = "Nairobi, Kenya", RegistDate = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc), Status = "Approved", Description = "Roof repairs for the main dormitory", CategoryId = 104, SupervisorId = 2 }
            );

            // Donations
            modelBuilder.Entity<Donation>().HasData(
                new Donation { Id = 301, Amount = 5000, Description = "Annual CSR contribution for Healthcare", Status = "Completed", Date = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc), DonorId = 103, CategoryId = 102, SupervisorId = 2 },
                new Donation { Id = 302, Amount = 1000, Description = "Personal gift for food drive", Status = "Completed", Date = new DateTime(2026, 3, 10, 0, 0, 0, DateTimeKind.Utc), DonorId = 101, CategoryId = 103, SupervisorId = 2 },
                new Donation { Id = 303, Amount = 2500, Description = "Emergency relief fund contribution", Status = "Pending", Date = new DateTime(2026, 4, 1, 0, 0, 0, DateTimeKind.Utc), DonorId = 102, CategoryId = 105, SupervisorId = 2 }
            );

            // Distributions
            modelBuilder.Entity<Distribution>().HasData(
                new Distribution { Id = 401, Amount = 3000, DistributionDate = new DateTime(2026, 3, 15, 0, 0, 0, DateTimeKind.Utc), Status = "Completed", Notes = "Funding provided for wheelchair procurement", CaseId = 201, DonationId = 301, HandledByEmployeeId = 3 },
                new Distribution { Id = 402, Amount = 500, DistributionDate = new DateTime(2026, 3, 20, 0, 0, 0, DateTimeKind.Utc), Status = "Completed", Notes = "First monthly food basket distribution", CaseId = 202, DonationId = 302, HandledByEmployeeId = 3 }
            );
        }
    }
}
