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
}
