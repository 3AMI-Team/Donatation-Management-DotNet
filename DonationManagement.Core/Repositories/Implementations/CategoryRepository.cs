using DonationManagement.Core.Entities;
using DonationManagement.Core.Repositories.Interfaces;
using DonationManagement.Core.Data;

namespace DonationManagement.Core.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DonationDbContext context) : base(context)
        {
        }
    }
}
