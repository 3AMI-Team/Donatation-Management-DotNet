using Microsoft.EntityFrameworkCore;
using DonationManagement.Api.DTOs;
using DonationManagement.Core;
using DonationManagement.Core.Entities;

namespace DonationManagement.Api.Services
{
    public class DistributionService : IDistributionService
    {
        private readonly DonationDbContext _context;

        public DistributionService(DonationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DistributionResponse>> GetAllDistributionsAsync()
        {
            return await _context.Distributions
                .Include(d => d.Case)
                .Include(d => d.HandledByEmployee)
                .Select(d => new DistributionResponse(d.Id, d.Amount, d.DistributionDate, d.Status, d.Recipient, d.CaseId, d.HandledByEmployeeId))
                .ToListAsync();
        }

        public async Task<DistributionResponse?> GetDistributionByIdAsync(int id)
        {
            var distribution = await _context.Distributions
                .Include(d => d.Case)
                .Include(d => d.HandledByEmployee)
                .FirstOrDefaultAsync(d => d.Id == id);

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

            _context.Distributions.Add(distribution);
            await _context.SaveChangesAsync();

            return new DistributionResponse(distribution.Id, distribution.Amount, distribution.DistributionDate, distribution.Status, distribution.Recipient, distribution.CaseId, distribution.HandledByEmployeeId);
        }

        public async Task<DistributionResponse?> UpdateDistributionAsync(int id, DistributionRequest request)
        {
            var distribution = await _context.Distributions.FindAsync(id);
            if (distribution == null) return null;

            distribution.Amount = request.Amount;
            distribution.DistributionDate = request.DistributionDate;
            distribution.Status = request.Status;
            distribution.Recipient = request.Recipient;
            distribution.CaseId = request.CaseId;
            distribution.HandledByEmployeeId = request.HandledByEmployeeId;

            await _context.SaveChangesAsync();

            return new DistributionResponse(distribution.Id, distribution.Amount, distribution.DistributionDate, distribution.Status, distribution.Recipient, distribution.CaseId, distribution.HandledByEmployeeId);
        }

        public async Task<bool> DeleteDistributionAsync(int id)
        {
            var distribution = await _context.Distributions.FindAsync(id);
            if (distribution == null) return false;

            _context.Distributions.Remove(distribution);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DistributionResponse>> GetDistributionsByCaseAsync(int caseId)
        {
            return await _context.Distributions
                .Where(d => d.CaseId == caseId)
                .Select(d => new DistributionResponse(d.Id, d.Amount, d.DistributionDate, d.Status, d.Recipient, d.CaseId, d.HandledByEmployeeId))
                .ToListAsync();
        }

        public async Task<DistributionResponse[]> DistributeEvenlyAsync(EvenDistributionRequest request)
        {
            // Get all open cases
            var openCases = await _context.Cases
                .Where(c => c.Status == "Open")
                .ToListAsync();

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
                _context.Distributions.Add(distribution);
            }

            await _context.SaveChangesAsync();

            return distributions
                .Select(d => new DistributionResponse(d.Id, d.Amount, d.DistributionDate, d.Status, d.Recipient, d.CaseId, d.HandledByEmployeeId))
                .ToArray();
        }
    }
}
