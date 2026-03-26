using DonationManagement.Core.Entities;
using DonationManagement.Core.Repositories.Interfaces;

namespace DonationManagement.Core.Repositories.Implementations
{
    public class CaseRepository : Repository<Case>, ICaseRepository
    {
        public CaseRepository(DonationDbContext context) : base(context)
        {
        }
    }
}
