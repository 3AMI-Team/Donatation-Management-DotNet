using System;
using System.Collections.Generic;

namespace DonationManagement.Core.Entities
{
    /// <summary>
    /// Represents a beneficiary case that needs support
    /// </summary>
    public class Case
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;            // Beneficiary Name
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime RegistDate { get; set; }
        public string Status { get; set; } = string.Empty;          // e.g., "Pending Review", "Approved", "Funded", "Closed"
        public string Description { get; set; } = string.Empty;

        // Foreign Keys
        public int CategoryId { get; set; }                         // Case Category
        public int? SupervisorId { get; set; }                      // Registered by supervisor (0..1)

        // Navigation
        public Category Category { get; set; } = null!;
        public Employee? Supervisor { get; set; }

        // A case can receive many distributions
        public ICollection<Distribution> Distributions { get; set; } = new List<Distribution>();
    }
}
