using DonationManagement.Core.Entities;

namespace DonationManagement.Core.Repositories
{
    public class CaseRepository : Repository<Case>, ICaseRepository
    {
        public CaseRepository(DonationDbContext context) : base(context) { }
    }
}
