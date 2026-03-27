using DonationManagement.Core.Entities;
using DonationManagement.Core.Repositories.Interfaces;
using DonationManagement.Core.Data;

namespace DonationManagement.Core.Repositories.Implementations
{
    public class DonorRepository : Repository<Donor>, IDonorRepository
    {
        public DonorRepository(DonationDbContext context) : base(context)
        {
        }
    }
}
