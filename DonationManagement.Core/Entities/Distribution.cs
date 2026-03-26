using System;

namespace DonationManagement.Core.Entities
{
    /// <summary>
    /// Represents a distribution of funds to a case
    /// (money collected from donors is distributed evenly to cases)
    /// </summary>
    public class Distribution
    {
        public int Id { get; set; }                 // Distribution_ID
        public decimal Amount { get; set; }          // Amount distributed
        public DateTime DistributionDate { get; set; }
        public string Status { get; set; } = string.Empty;           // e.g., "Pending", "Completed"
        public string Recipient { get; set; } = string.Empty;        // Name of person/organization receiving aid

        // Foreign Keys
        public int CaseId { get; set; }               // Which case this distribution belongs to (1)
        public int? HandledByEmployeeId { get; set; }  // Employee who handled it (0..1)

        // Navigation
        public Case Case { get; set; } = null!;
        public Employee? HandledByEmployee { get; set; }
    }
}
