namespace DonationManagement.Api.Services.Interfaces
{
    public interface IDistributionService
    {
        Task<IEnumerable<DistributionResponse>> GetAllDistributionsAsync();
        Task<PaginatedResponse<DistributionResponse>> GetDistributionsPagedAsync(int page, int pageSize);
        Task<DistributionResponse?> GetDistributionByIdAsync(int id);
        Task<DistributionResponse> CreateDistributionAsync(DistributionRequest request);
        Task<DistributionResponse?> UpdateDistributionAsync(int id, DistributionRequest request);
        Task<bool> DeleteDistributionAsync(int id);
        Task<IEnumerable<DistributionResponse>> GetDistributionsByCaseAsync(int caseId);
        Task<DistributionResponse[]> DistributeEvenlyAsync(EvenDistributionRequest request);
    }
}
