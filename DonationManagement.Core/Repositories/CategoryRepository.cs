using DonationManagement.Core.Entities;

namespace DonationManagement.Core.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DonationDbContext context) : base(context) { }
    }
}
