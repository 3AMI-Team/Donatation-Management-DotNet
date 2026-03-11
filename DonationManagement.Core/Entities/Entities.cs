using System;
using System.Collections.Generic;

namespace DonationManagement.Core.Entities
{
    /// <summary>
    /// Represents an employee (can be a supervisor or worker)
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }                // Employee_ID
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;            // e.g., "Supervisor", "Worker"
        public string Password { get; set; } = string.Empty;        // Hashed
        public string Username { get; set; } = string.Empty;

        // Navigation: an employee can register many cases (if supervisor)
        public ICollection<Case> RegisteredCases { get; set; } = new List<Case>();

        // Navigation: an employee can handle many distributions
        public ICollection<Distribution> DistributionsHandled { get; set; } = new List<Distribution>();
    }

    /// <summary>
    /// Represents a donor who contributes money
    /// </summary>
    public class Donor
    {
        public int Id { get; set; }                // Donor_ID
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime RegisterDate { get; set; }

        // Navigation: a donor can be associated with many cases
        public ICollection<Case> Cases { get; set; } = new List<Case>();
    }

    /// <summary>
    /// Represents a category that classifies cases (e.g., Medical, Education)
    /// </summary>
    public class Category
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;            // e.g., "Medical", "Education"
        public string Description { get; set; } = string.Empty;

        // Navigation: a category classifies many cases
        public ICollection<Case> Cases { get; set; } = new List<Case>();
    }

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
