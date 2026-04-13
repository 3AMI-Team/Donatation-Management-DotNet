using DonationManagement.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonationManagement.Api.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardResponse> GetFullDashboardDataAsync();
        Task<DashboardKpis> GetKpisAsync();
        Task<List<RecentDonationResponse>> GetLastDonationsAsync();
        Task<List<RecentDistributionResponse>> GetLastDistributionsAsync();
        Task<DonationTrends> GetTrendsAsync();
    }
}
