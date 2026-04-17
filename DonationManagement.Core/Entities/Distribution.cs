using System;

namespace DonationManagement.Core.Entities
{
    /// <summary>
    /// Represents a distribution of funds to a case beneficiary
    /// </summary>
    public class Distribution
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DistributionDate { get; set; }
        public string Status { get; set; } = string.Empty;           // e.g., "Pending", "Completed"
        public string Notes { get; set; } = string.Empty;

        // Foreign Keys
        public int CaseId { get; set; }               // Which beneficiary case receives the distribution
        public int DonationId { get; set; }           // Which donation funding this distribution
        public int? HandledByEmployeeId { get; set; }  // Employee who handles it (0..1)

        // Navigation
        public Case Case { get; set; } = null!;
        public Donation Donation { get; set; } = null!;
        public Employee? HandledByEmployee { get; set; }
    }
}
