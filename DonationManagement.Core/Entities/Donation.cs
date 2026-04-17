using System;
using System.Collections.Generic;

namespace DonationManagement.Core.Entities
{
    /// <summary>
    /// Represents a donation record from a donor
    /// </summary>
    public class Donation
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;          // e.g., "Pending", "Completed"
        public DateTime Date { get; set; }

        // Foreign Keys
        public int? SupervisorId { get; set; }      // Employee who registered the donation
        public int DonorId { get; set; }            // Donor who made the donation
        public int CategoryId { get; set; }         // Category of the donation

        // Navigation
        public Employee? Supervisor { get; set; }
        public Donor Donor { get; set; } = null!;
        public Category Category { get; set; } = null!;

        // A donation can be used in many distributions
        public ICollection<Distribution> Distributions { get; set; } = new List<Distribution>();
    }
}
