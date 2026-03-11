using DonationManagement.Api.DTOs;

namespace DonationManagement.Api.Services
{
    public interface IDonorService
    {
        Task<IEnumerable<DonorResponse>> GetAllDonorsAsync();
        Task<DonorResponse?> GetDonorByIdAsync(int id);
        Task<DonorResponse> CreateDonorAsync(DonorRequest request);
        Task<DonorResponse?> UpdateDonorAsync(int id, DonorRequest request);
        Task<bool> DeleteDonorAsync(int id);
        Task<IEnumerable<CaseResponse>> GetDonorCasesAsync(int donorId);
    }
}
