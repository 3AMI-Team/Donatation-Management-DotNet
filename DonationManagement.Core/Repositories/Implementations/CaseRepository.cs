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
    public class CaseRepository : Repository<Case>, ICaseRepository
    {
        public CaseRepository(DonationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Case>> GetAllAsync()
        {
            return await _dbSet
                .Include(c => c.Category)
                .Include(c => c.Supervisor)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Case>> GetPagedAsync(int page, int pageSize)
        {
            return await _dbSet
                .Include(c => c.Category)
                .Include(c => c.Supervisor)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public override async Task<Case?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(c => c.Category)
                .Include(c => c.Supervisor)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public override async Task<IEnumerable<Case>> FindAsync(Expression<Func<Case, bool>> predicate)
        {
            return await _dbSet
                .Include(c => c.Category)
                .Include(c => c.Supervisor)
                .Where(predicate)
                .ToListAsync();
        }
    }
}
