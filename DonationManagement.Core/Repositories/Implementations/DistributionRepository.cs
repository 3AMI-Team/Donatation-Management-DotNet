using DonationManagement.Core.Entities;
using DonationManagement.Core.Repositories.Interfaces;
using DonationManagement.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace DonationManagement.Core.Repositories.Implementations
{
    public class DistributionRepository : Repository<Distribution>, IDistributionRepository
    {
        public DistributionRepository(DonationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Distribution>> GetAllAsync()
        {
            return await _dbSet
                .Include(d => d.Case)
                .Include(d => d.Donation)
                    .ThenInclude(don => don.Donor)
                .Include(d => d.HandledByEmployee)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Distribution>> GetPagedAsync(int page, int pageSize)
        {
            return await _dbSet
                .Include(d => d.Case)
                .Include(d => d.Donation)
                    .ThenInclude(don => don.Donor)
                .Include(d => d.HandledByEmployee)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public override async Task<Distribution?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(d => d.Case)
                .Include(d => d.Donation)
                    .ThenInclude(don => don.Donor)
                .Include(d => d.HandledByEmployee)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public override async Task<IEnumerable<Distribution>> FindAsync(Expression<Func<Distribution, bool>> predicate)
        {
            return await _dbSet
                .Include(d => d.Case)
                .Include(d => d.Donation)
                    .ThenInclude(don => don.Donor)
                .Include(d => d.HandledByEmployee)
                .Where(predicate)
                .ToListAsync();
        }
    }
}
