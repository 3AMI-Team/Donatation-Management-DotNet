namespace DonationManagement.Api.Services.Interfaces
{
    public interface ICaseService
    {
        Task<IEnumerable<CaseResponse>> GetAllCasesAsync();
        Task<PaginatedResponse<CaseResponse>> GetCasesPagedAsync(int page, int pageSize);
        Task<CaseResponse?> GetCaseByIdAsync(int id);
        Task<CaseResponse> CreateCaseAsync(CaseRequest request);
        Task<CaseResponse?> UpdateCaseAsync(int id, CaseRequest request);
        Task<bool> DeleteCaseAsync(int id);
        Task<IEnumerable<DistributionResponse>> GetCaseDistributionsAsync(int caseId);
        Task<decimal> GetRemainingAmountNeededAsync(int caseId);
        Task<bool> IsFullyFundedAsync(int caseId);
    }
}
