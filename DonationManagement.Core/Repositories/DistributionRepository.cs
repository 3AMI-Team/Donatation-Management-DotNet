using DonationManagement.Core.Entities;

namespace DonationManagement.Core.Repositories
{
    public class DistributionRepository : Repository<Distribution>, IDistributionRepository
    {
        public DistributionRepository(DonationDbContext context) : base(context) { }
    }
}
