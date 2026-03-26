using DonationManagement.Api.DTOs;
using DonationManagement.Core;
using DonationManagement.Core.Entities;
using DonationManagement.Core.Repositories;

namespace DonationManagement.Api.Services
{
    public class DistributionService : IDistributionService
    {
        private readonly IDistributionRepository _distributionRepo;
        private readonly ICaseRepository _caseRepo;

        public DistributionService(IDistributionRepository distributionRepo, ICaseRepository caseRepo)
        {
            _distributionRepo = distributionRepo;
            _caseRepo = caseRepo;
        }

        public async Task<IEnumerable<DistributionResponse>> GetAllDistributionsAsync()
        {
            var distributions = await _distributionRepo.GetAllAsync();
            return distributions.Select(d => new DistributionResponse(d.Id, d.Amount, d.DistributionDate, d.Status, d.Recipient, d.CaseId, d.HandledByEmployeeId));
        }

        public async Task<PaginatedResponse<DistributionResponse>> GetDistributionsPagedAsync(int page, int pageSize)
        {
            var (normalizedPage, normalizedPageSize) = Pagination.Normalize(page, pageSize);
            var totalCount = await _distributionRepo.CountAsync();

            var distributions = await _distributionRepo.GetAllAsync();
            var items = distributions
                .OrderBy(d => d.Id)
                .Skip((normalizedPage - 1) * normalizedPageSize)
                .Take(normalizedPageSize)
                .Select(d => new DistributionResponse(d.Id, d.Amount, d.DistributionDate, d.Status, d.Recipient, d.CaseId, d.HandledByEmployeeId))
                .ToList();

            var totalPages = Pagination.GetTotalPages(totalCount, normalizedPageSize);
            return new PaginatedResponse<DistributionResponse>(items, normalizedPage, normalizedPageSize, totalCount, totalPages);
        }

        public async Task<DistributionResponse?> GetDistributionByIdAsync(int id)
        {
            var distribution = await _distributionRepo.GetByIdAsync(id);
            return distribution == null ? null :
                new DistributionResponse(distribution.Id, distribution.Amount, distribution.DistributionDate, distribution.Status, distribution.Recipient, distribution.CaseId, distribution.HandledByEmployeeId);
        }

        public async Task<DistributionResponse> CreateDistributionAsync(DistributionRequest request)
        {
            var distribution = new Distribution
            {
                Amount = request.Amount,
                DistributionDate = request.DistributionDate,
                Status = request.Status,
                Recipient = request.Recipient,
                CaseId = request.CaseId,
                HandledByEmployeeId = request.HandledByEmployeeId
            };

            await _distributionRepo.AddAsync(distribution);
            await _distributionRepo.SaveChangesAsync();

            return new DistributionResponse(distribution.Id, distribution.Amount, distribution.DistributionDate, distribution.Status, distribution.Recipient, distribution.CaseId, distribution.HandledByEmployeeId);
        }

        public async Task<DistributionResponse?> UpdateDistributionAsync(int id, DistributionRequest request)
        {
            var distribution = await _distributionRepo.GetByIdAsync(id);
            if (distribution == null) return null;

            distribution.Amount = request.Amount;
            distribution.DistributionDate = request.DistributionDate;
            distribution.Status = request.Status;
            distribution.Recipient = request.Recipient;
            distribution.CaseId = request.CaseId;
            distribution.HandledByEmployeeId = request.HandledByEmployeeId;

            _distributionRepo.Update(distribution);
            await _distributionRepo.SaveChangesAsync();

            return new DistributionResponse(distribution.Id, distribution.Amount, distribution.DistributionDate, distribution.Status, distribution.Recipient, distribution.CaseId, distribution.HandledByEmployeeId);
        }

        public async Task<bool> DeleteDistributionAsync(int id)
        {
            var distribution = await _distributionRepo.GetByIdAsync(id);
            if (distribution == null) return false;

            _distributionRepo.Remove(distribution);
            await _distributionRepo.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DistributionResponse>> GetDistributionsByCaseAsync(int caseId)
        {
            var distributions = await _distributionRepo.FindAsync(d => d.CaseId == caseId);
            return distributions.Select(d => new DistributionResponse(d.Id, d.Amount, d.DistributionDate, d.Status, d.Recipient, d.CaseId, d.HandledByEmployeeId));
        }

        public async Task<DistributionResponse[]> DistributeEvenlyAsync(EvenDistributionRequest request)
        {
            // Get all open cases
            var openCasesList = await _caseRepo.FindAsync(c => c.Status == "Open");
            var openCases = openCasesList.ToList();

            if (!openCases.Any()) return Array.Empty<DistributionResponse>();

            var totalCases = openCases.Count;
            var amountPerCase = request.TotalAmount / totalCases;
            var distributionDate = DateTime.UtcNow;

            if (!request.AutoDistribute)
            {
                return openCases
                    .Select(c => new DistributionResponse(
                        0,
                        amountPerCase,
                        distributionDate,
                        "Pending",
                        $"Case {c.Id}",
                        c.Id,
                        null))
                    .ToArray();
            }

            var distributions = new List<Distribution>();

            foreach (var caseEntity in openCases)
            {
                var distribution = new Distribution
                {
                    Amount = amountPerCase,
                    DistributionDate = distributionDate,
                    Status = "Pending",
                    Recipient = $"Case {caseEntity.Id}",
                    CaseId = caseEntity.Id,
                    HandledByEmployeeId = null // Could be set to current employee if available
                };

                distributions.Add(distribution);
                await _distributionRepo.AddAsync(distribution);
            }

            await _distributionRepo.SaveChangesAsync();

            return distributions
                .Select(d => new DistributionResponse(d.Id, d.Amount, d.DistributionDate, d.Status, d.Recipient, d.CaseId, d.HandledByEmployeeId))
                .ToArray();
        }
    }
}
