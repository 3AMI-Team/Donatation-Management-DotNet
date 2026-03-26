using DonationManagement.Api.DTOs;
using DonationManagement.Api.Mappings;
using DonationManagement.Api.Services;
using DonationManagement.Api.Services.Interfaces;
using DonationManagement.Core;
using DonationManagement.Core.Entities;
using DonationManagement.Core.Repositories.Interfaces;

namespace DonationManagement.Api.Services.Implementations
{
    public class CaseService : ICaseService
    {
        private readonly ICaseRepository _caseRepo;
        private readonly IDistributionRepository _distributionRepo;

        public CaseService(ICaseRepository caseRepo, IDistributionRepository distributionRepo)
        {
            _caseRepo = caseRepo;
            _distributionRepo = distributionRepo;
        }

        public async Task<IEnumerable<CaseResponse>> GetAllCasesAsync()
        {
            var cases = await _caseRepo.GetAllAsync();
            return cases.Select(c => c.ToResponse());
        }

        public async Task<PaginatedResponse<CaseResponse>> GetCasesPagedAsync(int page, int pageSize)
        {
            var (normalizedPage, normalizedPageSize) = Pagination.Normalize(page, pageSize);
            var totalCount = await _caseRepo.CountAsync();

            var cases = await _caseRepo.GetPagedAsync(normalizedPage, normalizedPageSize);
            var items = cases.Select(c => c.ToResponse()).ToList();

            var totalPages = Pagination.GetTotalPages(totalCount, normalizedPageSize);
            return new PaginatedResponse<CaseResponse>(items, normalizedPage, normalizedPageSize, totalCount, totalPages);
        }

        public async Task<CaseResponse?> GetCaseByIdAsync(int id)
        {
            var caseEntity = await _caseRepo.GetByIdAsync(id);
            return caseEntity?.ToResponse();
        }

        public async Task<CaseResponse> CreateCaseAsync(CaseRequest request)
        {
            var caseEntity = new Case
            {
                Amount = request.Amount,
                Description = request.Description,
                Status = request.Status,
                Date = request.Date,
                SupervisorId = request.SupervisorId,
                DonorId = request.DonorId,
                CategoryId = request.CategoryId
            };

            await _caseRepo.AddAsync(caseEntity);
            await _caseRepo.SaveChangesAsync();

            return caseEntity.ToResponse();
        }

        public async Task<CaseResponse?> UpdateCaseAsync(int id, CaseRequest request)
        {
            var caseEntity = await _caseRepo.GetByIdAsync(id);
            if (caseEntity == null) return null;

            caseEntity.Amount = request.Amount;
            caseEntity.Description = request.Description;
            caseEntity.Status = request.Status;
            caseEntity.Date = request.Date;
            caseEntity.SupervisorId = request.SupervisorId;
            caseEntity.DonorId = request.DonorId;
            caseEntity.CategoryId = request.CategoryId;

            _caseRepo.Update(caseEntity);
            await _caseRepo.SaveChangesAsync();

            return caseEntity.ToResponse();
        }

        public async Task<bool> DeleteCaseAsync(int id)
        {
            var caseEntity = await _caseRepo.GetByIdAsync(id);
            if (caseEntity == null) return false;

            _caseRepo.Remove(caseEntity);
            await _caseRepo.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DistributionResponse>> GetCaseDistributionsAsync(int caseId)
        {
            var distributions = await _distributionRepo.FindAsync(d => d.CaseId == caseId);
            return distributions.Select(d => d.ToResponse());
        }

        public async Task<decimal> GetRemainingAmountNeededAsync(int caseId)
        {
            var caseEntity = await _caseRepo.GetByIdAsync(caseId);
            if (caseEntity == null) return 0;

            var distributions = await _distributionRepo.FindAsync(d => d.CaseId == caseId && d.Status == "Completed");
            var totalDistributed = distributions.Sum(d => d.Amount);

            return Math.Max(0, caseEntity.Amount - totalDistributed);
        }

        public async Task<bool> IsFullyFundedAsync(int caseId)
        {
            var remaining = await GetRemainingAmountNeededAsync(caseId);
            return remaining == 0;
        }
    }
}
