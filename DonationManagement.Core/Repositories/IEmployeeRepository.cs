using DonationManagement.Core.Entities;

namespace DonationManagement.Core.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetByRoleAsync(string role);
    }
}
