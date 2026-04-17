using DonationManagement.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonationManagement.Api.Services.Interfaces
{
    public interface IDistributionService
    {
        Task<IEnumerable<DistributionResponse>> GetAllDistributionsAsync();
        Task<DistributionResponse?> GetDistributionByIdAsync(int id);
        Task<DistributionResponse> CreateDistributionAsync(DistributionRequest request);
        Task<DistributionResponse?> UpdateDistributionAsync(int id, DistributionRequest request);
        Task<bool> DeleteDistributionAsync(int id);
        Task<DistributionKpis> GetDistributionKpisAsync();
    }
}
