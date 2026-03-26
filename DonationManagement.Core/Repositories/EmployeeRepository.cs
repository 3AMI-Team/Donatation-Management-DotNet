using DonationManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DonationManagement.Core.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DonationDbContext context) : base(context) { }

        public async Task<IEnumerable<Employee>> GetByRoleAsync(string role)
        {
            return await _dbSet.Where(e => e.Role == role).ToListAsync();
        }
    }
}
