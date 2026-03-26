using System.Collections.Generic;

namespace DonationManagement.Core.Entities
{
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
}
