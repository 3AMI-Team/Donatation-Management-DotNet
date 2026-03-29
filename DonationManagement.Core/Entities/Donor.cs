using System;
using System.Collections.Generic;

namespace DonationManagement.Core.Entities
{
    /// <summary>
    /// Represents a donor who contributes money
    /// </summary>
    public class Donor
    {
        public int Id { get; set; }                // Donor_ID
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime RegisterDate { get; set; }

        // Navigation: a donor can be associated with many cases
        public ICollection<Case> Cases { get; set; } = new List<Case>();
    }
}
