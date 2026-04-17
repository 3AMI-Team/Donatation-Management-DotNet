using DonationManagement.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonationManagement.Api.Services.Interfaces
{
    public interface IDonationService
    {
        Task<IEnumerable<DonationResponse>> GetAllDonationsAsync(int? categoryId = null);
        Task<PaginatedResponse<DonationResponse>> GetDonationsPagedAsync(int page, int pageSize, int? categoryId = null);
        Task<DonationResponse?> GetDonationByIdAsync(int id);
        Task<DonationResponse> CreateDonationAsync(DonationRequest request);
        Task<DonationResponse?> UpdateDonationAsync(int id, DonationRequest request);
        Task<bool> DeleteDonationAsync(int id);
        Task<DonationKpis> GetDonationKpisAsync();
    }
}
