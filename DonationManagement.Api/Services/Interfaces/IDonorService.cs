using DonationManagement.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonationManagement.Api.Services.Interfaces
{
    public interface IDonorService
    {
        Task<IEnumerable<DonorResponse>> GetAllDonorsAsync();
        Task<PaginatedResponse<DonorResponse>> GetDonorsPagedAsync(int page, int pageSize);
        Task<DonorResponse?> GetDonorByIdAsync(int id);
        Task<DonorResponse> CreateDonorAsync(DonorRequest request);
        Task<DonorResponse?> UpdateDonorAsync(int id, DonorRequest request);
        Task<bool> DeleteDonorAsync(int id);
        
        // Updated to use Donations
        Task<IEnumerable<DonationResponse>> GetDonorDonationsAsync(int donorId);
        
        Task<DonorResponse> SignupAsync(DonorSignupRequest request);
        Task<AuthResponse?> LoginAsync(DonorLoginRequest request);
        Task<DonorKpis> GetDonorKpisAsync();
    }
}
