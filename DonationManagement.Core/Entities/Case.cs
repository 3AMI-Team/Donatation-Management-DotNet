using System;
using System.Collections.Generic;

namespace DonationManagement.Core.Entities
{
    /// <summary>
    /// Represents a case that needs funding
    /// </summary>
    public class Case
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }         // Total amount needed/pledged
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;          // e.g., "Open", "Closed", "In Progress"
        public DateTime Date { get; set; }          // Case creation date

        // Foreign Keys
        public int? SupervisorId { get; set; }      // Employee who registered (0..1)
        public int DonorId { get; set; }            // Donor associated (1)
        public int CategoryId { get; set; }         // Category (1)

        // Navigation
        public Employee? Supervisor { get; set; }
        public Donor Donor { get; set; } = null!;
        public Category Category { get; set; } = null!;

        // A case can receive many distributions
        public ICollection<Distribution> Distributions { get; set; } = new List<Distribution>();
    }
}
