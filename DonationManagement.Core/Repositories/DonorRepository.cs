using DonationManagement.Core.Entities;

namespace DonationManagement.Core.Repositories
{
    public class DonorRepository : Repository<Donor>, IDonorRepository
    {
        public DonorRepository(DonationDbContext context) : base(context) { }
    }
}
