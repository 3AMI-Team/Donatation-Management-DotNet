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
    public class DonationRepository : Repository<Donation>, IDonationRepository
    {
        public DonationRepository(DonationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Donation>> GetAllAsync()
        {
            return await _dbSet
                .Include(d => d.Donor)
                .Include(d => d.Category)
                .Include(d => d.Supervisor)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Donation>> GetPagedAsync(int page, int pageSize)
        {
            return await _dbSet
                .Include(d => d.Donor)
                .Include(d => d.Category)
                .Include(d => d.Supervisor)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public override async Task<Donation?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(d => d.Donor)
                .Include(d => d.Category)
                .Include(d => d.Supervisor)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public override async Task<IEnumerable<Donation>> FindAsync(Expression<Func<Donation, bool>> predicate)
        {
            return await _dbSet
                .Include(d => d.Donor)
                .Include(d => d.Category)
                .Include(d => d.Supervisor)
                .Where(predicate)
                .ToListAsync();
        }
    }
}
