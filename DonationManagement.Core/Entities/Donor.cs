using System;
using System.Collections.Generic;

namespace DonationManagement.Core.Entities
{
    /// <summary>
    /// Represents a donor who contributes money
    /// </summary>
    public class Donor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;            // e.g., "Individual", "Corporate"
        public DateTime RegisterDate { get; set; }

        // Navigation: a donor makes many donations
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    }
}
