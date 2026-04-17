using System.Collections.Generic;

namespace DonationManagement.Core.Entities
{
    /// <summary>
    /// Represents a category that classifies donations and cases (e.g., Medical, Education)
    /// </summary>
    public class Category
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;            // e.g., "Medical", "Education"
        public string Description { get; set; } = string.Empty;

        // Navigation: a category classifies many donations
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();

        // Navigation: a category classifies many beneficiary cases
        public ICollection<Case> Cases { get; set; } = new List<Case>();
    }
}
