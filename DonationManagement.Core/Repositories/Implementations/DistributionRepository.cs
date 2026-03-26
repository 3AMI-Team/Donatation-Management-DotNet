using DonationManagement.Core.Entities;
using DonationManagement.Core.Repositories.Interfaces;

namespace DonationManagement.Core.Repositories.Implementations
{
    public class DistributionRepository : Repository<Distribution>, IDistributionRepository
    {
        public DistributionRepository(DonationDbContext context) : base(context)
        {
        }
    }
}
