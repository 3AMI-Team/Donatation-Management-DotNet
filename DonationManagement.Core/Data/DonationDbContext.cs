using Microsoft.EntityFrameworkCore;
using DonationManagement.Core.Entities;

namespace DonationManagement.Core.Data
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
            base.OnModelCreating(modelBuilder);

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

            // Configure decimal precision
            modelBuilder.Entity<Case>()
                .Property(c => c.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Distribution>()
                .Property(d => d.Amount)
                .HasPrecision(18, 2);

            // Shared configuration
            var sharedPasswordHash = "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W"; // 12345678

          
            // Seed Admin User
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Ibrahim Nasser",
                    Username = "Ibrahim",
                    Password = "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W",
                    Role = "Admin",
                    Phone = "01278988474",
                    Address = "Ismailia, Egypt",
                    Email = "12baraka34@gmail.com"
                },
                new Employee
                {
                    Id = 2,
                    Name = "Sarah Connor",
                    Username = "SarahC",
                    Password = sharedPasswordHash,
                    Role = "Supervisor",
                    Phone = "+12025550199",
                    Address = "New York, USA",
                    Email = "sarah.c@example.com"
                },
                new Employee
                {
                    Id = 3,
                    Name = "John Doe",
                    Username = "JohnD",
                    Password = sharedPasswordHash,
                    Role = "FieldWorker",
                    Phone = "+12025550188",
                    Address = "London, UK",
                    Email = "john.doe@example.com"
                }
            );
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 101, Type = "Education", Description = "Student sponsorship and school supplies" },
                new Category { Id = 102, Type = "Healthcare", Description = "Medications, surgeries, and medical equipment" },
                new Category { Id = 103, Type = "Food Security", Description = "Food packages and meal distributions" },
                new Category { Id = 104, Type = "Housing", Description = "Home renovation and clean water access" },
                new Category { Id = 105, Type = "Emergency Relief", Description = "Disaster response and urgent assistance" }
            );

            // Seed Donors
            modelBuilder.Entity<Donor>().HasData(
                new Donor { Id = 101, Name = "James Wilson", Email = "james.w@example.com", Password = sharedPasswordHash, Phone = "+12025550101", RegisterDate = new DateTime(2025, 1, 10, 10, 0, 0, DateTimeKind.Utc) },
                new Donor { Id = 102, Name = "Mary Johnson", Email = "mary.j@example.com", Password = sharedPasswordHash, Phone = "+12025550102", RegisterDate = new DateTime(2025, 2, 5, 12, 30, 0, DateTimeKind.Utc) },
                new Donor { Id = 103, Name = "Robert Smith", Email = "robert.s@example.com", Password = sharedPasswordHash, Phone = "+12025550103", RegisterDate = new DateTime(2025, 3, 12, 9, 15, 0, DateTimeKind.Utc) },
                new Donor { Id = 104, Name = "Patricia Brown", Email = "patricia.b@example.com", Password = sharedPasswordHash, Phone = "+12025550104", RegisterDate = new DateTime(2025, 4, 18, 14, 45, 0, DateTimeKind.Utc) },
                new Donor { Id = 105, Name = "Michael Davis", Email = "michael.d@example.com", Password = sharedPasswordHash, Phone = "+12025550105", RegisterDate = new DateTime(2025, 5, 20, 16, 20, 0, DateTimeKind.Utc) },
                new Donor { Id = 106, Name = "Linda Miller", Email = "linda.m@example.com", Password = sharedPasswordHash, Phone = "+12025550106", RegisterDate = new DateTime(2025, 6, 22, 11, 10, 0, DateTimeKind.Utc) },
                new Donor { Id = 107, Name = "David Taylor", Email = "david.t@example.com", Password = sharedPasswordHash, Phone = "+12025550107", RegisterDate = new DateTime(2025, 7, 30, 8, 50, 0, DateTimeKind.Utc) },
                new Donor { Id = 108, Name = "Elizabeth Anderson", Email = "elizabeth.a@example.com", Password = sharedPasswordHash, Phone = "+12025550108", RegisterDate = new DateTime(2025, 8, 14, 13, 25, 0, DateTimeKind.Utc) },
                new Donor { Id = 109, Name = "Richard Thomas", Email = "richard.t@example.com", Password = sharedPasswordHash, Phone = "+12025550109", RegisterDate = new DateTime(2025, 9, 5, 15, 55, 0, DateTimeKind.Utc) },
                new Donor { Id = 110, Name = "Barbara Jackson", Email = "barbara.j@example.com", Password = sharedPasswordHash, Phone = "+12025550110", RegisterDate = new DateTime(2025, 10, 1, 10, 5, 0, DateTimeKind.Utc) }
            );

            // Seed Cases
            modelBuilder.Entity<Case>().HasData(
                new Case { Id = 101, Amount = 3000, Description = "Sponsorship for a struggling university student", Status = "Open", Date = new DateTime(2026, 1, 15, 0, 0, 0, DateTimeKind.Utc), DonorId = 101, CategoryId = 101, SupervisorId = 1 },
                new Case { Id = 102, Amount = 15000, Description = "Heart surgery for an elderly patient", Status = "In Progress", Date = new DateTime(2026, 1, 20, 0, 0, 0, DateTimeKind.Utc), DonorId = 102, CategoryId = 102, SupervisorId = 1 },
                new Case { Id = 103, Amount = 500, Description = "Monthly food basket for a family in need", Status = "Closed", Date = new DateTime(2026, 1, 25, 0, 0, 0, DateTimeKind.Utc), DonorId = 103, CategoryId = 103, SupervisorId = 1 },
                new Case { Id = 104, Amount = 7000, Description = "Installing a clean water well in a rural village", Status = "Open", Date = new DateTime(2026, 2, 2, 0, 0, 0, DateTimeKind.Utc), DonorId = 104, CategoryId = 104, SupervisorId = 1 },
                new Case { Id = 105, Amount = 1200, Description = "Winter clothing drive for orphanages", Status = "Open", Date = new DateTime(2026, 2, 5, 0, 0, 0, DateTimeKind.Utc), DonorId = 105, CategoryId = 103, SupervisorId = 1 },
                new Case { Id = 106, Amount = 2500, Description = "Monthly insulin medication for diabetic patients", Status = "In Progress", Date = new DateTime(2026, 2, 10, 0, 0, 0, DateTimeKind.Utc), DonorId = 106, CategoryId = 102, SupervisorId = 1 },
                new Case { Id = 107, Amount = 1500, Description = "School uniforms and backpacks for 50 kids", Status = "Closed", Date = new DateTime(2026, 2, 18, 0, 0, 0, DateTimeKind.Utc), DonorId = 107, CategoryId = 101, SupervisorId = 1 },
                new Case { Id = 108, Amount = 4000, Description = "Repairing the roof of a collapsed house", Status = "Open", Date = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc), DonorId = 108, CategoryId = 104, SupervisorId = 1 },
                new Case { Id = 109, Amount = 800, Description = "Emergency food supply for a refugee family", Status = "Closed", Date = new DateTime(2026, 3, 4, 0, 0, 0, DateTimeKind.Utc), DonorId = 109, CategoryId = 103, SupervisorId = 1 },
                new Case { Id = 110, Amount = 6000, Description = "Furniture and basics for a newly built shelter", Status = "In Progress", Date = new DateTime(2026, 3, 12, 0, 0, 0, DateTimeKind.Utc), DonorId = 110, CategoryId = 104, SupervisorId = 1 },
                new Case { Id = 111, Amount = 3500, Description = "Laptops for high-achieving low-income students", Status = "Open", Date = new DateTime(2026, 3, 15, 0, 0, 0, DateTimeKind.Utc), DonorId = 101, CategoryId = 101, SupervisorId = 1 },
                new Case { Id = 112, Amount = 20000, Description = "Urgent diagnostic center for specialized tests", Status = "Open", Date = new DateTime(2026, 3, 22, 0, 0, 0, DateTimeKind.Utc), DonorId = 102, CategoryId = 102, SupervisorId = 1 },
                new Case { Id = 113, Amount = 4500, Description = "Clearing debts for single mothers", Status = "In Progress", Date = new DateTime(2026, 3, 28, 0, 0, 0, DateTimeKind.Utc), DonorId = 103, CategoryId = 105, SupervisorId = 1 },
                new Case { Id = 114, Amount = 1800, Description = "Online course subscriptions for skill dev", Status = "Open", Date = new DateTime(2026, 4, 1, 0, 0, 0, DateTimeKind.Utc), DonorId = 104, CategoryId = 101, SupervisorId = 1 },
                new Case { Id = 115, Amount = 9000, Description = "Solar panel installation for a community center", Status = "Open", Date = new DateTime(2026, 4, 3, 0, 0, 0, DateTimeKind.Utc), DonorId = 105, CategoryId = 104, SupervisorId = 1 },
                new Case { Id = 116, Amount = 1200, Description = "Wheelchairs for disabled athletes", Status = "Closed", Date = new DateTime(2026, 4, 5, 0, 0, 0, DateTimeKind.Utc), DonorId = 106, CategoryId = 102, SupervisorId = 1 },
                new Case { Id = 117, Amount = 5000, Description = "Restoration of a local library", Status = "In Progress", Date = new DateTime(2026, 4, 7, 0, 0, 0, DateTimeKind.Utc), DonorId = 107, CategoryId = 101, SupervisorId = 1 },
                new Case { Id = 118, Amount = 2500, Description = "Nutrition kits for pregnant women", Status = "Closed", Date = new DateTime(2026, 4, 9, 0, 0, 0, DateTimeKind.Utc), DonorId = 108, CategoryId = 103, SupervisorId = 1 },
                new Case { Id = 119, Amount = 4200, Description = "Vocational training for unemployed youth", Status = "Open", Date = new DateTime(2026, 4, 10, 0, 0, 0, DateTimeKind.Utc), DonorId = 109, CategoryId = 105, SupervisorId = 1 },
                new Case { Id = 120, Amount = 8000, Description = "Rehabilitation center for post-surgery recovery", Status = "Open", Date = new DateTime(2026, 4, 11, 0, 0, 0, DateTimeKind.Utc), DonorId = 110, CategoryId = 102, SupervisorId = 1 }
            );

            // Seed Distributions
            modelBuilder.Entity<Distribution>().HasData(
                new Distribution { Id = 101, Amount = 500, DistributionDate = new DateTime(2026, 1, 26, 0, 0, 0, DateTimeKind.Utc), Status = "Completed", Recipient = "Sarah Jenkins", CaseId = 103, HandledByEmployeeId = 1 },
                new Distribution { Id = 102, Amount = 1500, DistributionDate = new DateTime(2026, 2, 19, 0, 0, 0, DateTimeKind.Utc), Status = "Completed", Recipient = "City General Hospital", CaseId = 107, HandledByEmployeeId = 1 },
                new Distribution { Id = 103, Amount = 800, DistributionDate = new DateTime(2026, 3, 5, 0, 0, 0, DateTimeKind.Utc), Status = "Completed", Recipient = "Local Refugee Center", CaseId = 109, HandledByEmployeeId = 1 },
                new Distribution { Id = 104, Amount = 5000, DistributionDate = new DateTime(2026, 1, 22, 0, 0, 0, DateTimeKind.Utc), Status = "Processing", Recipient = "Health Services Dept", CaseId = 102, HandledByEmployeeId = 1 },
                new Distribution { Id = 105, Amount = 2000, DistributionDate = new DateTime(2026, 2, 12, 0, 0, 0, DateTimeKind.Utc), Status = "Completed", Recipient = "Diabetic Care Clinic", CaseId = 106, HandledByEmployeeId = 1 }
            );
        }
    }
}
