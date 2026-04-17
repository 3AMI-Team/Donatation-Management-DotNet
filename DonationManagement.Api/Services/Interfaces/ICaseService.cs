using DonationManagement.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonationManagement.Api.Services.Interfaces
{
    public interface ICaseService
    {
        Task<IEnumerable<CaseResponse>> GetAllCasesAsync(int? categoryId = null);
        Task<PaginatedResponse<CaseResponse>> GetCasesPagedAsync(int page, int pageSize, int? categoryId = null);
        Task<CaseResponse?> GetCaseByIdAsync(int id);
        Task<CaseResponse> CreateCaseAsync(CaseRequest request);
        Task<CaseResponse?> UpdateCaseAsync(int id, CaseRequest request);
        Task<bool> DeleteCaseAsync(int id);
    }
}
