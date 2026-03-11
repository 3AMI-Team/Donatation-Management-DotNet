using Microsoft.EntityFrameworkCore;
using DonationManagement.Api.DTOs;
using DonationManagement.Core;
using DonationManagement.Core.Entities;

namespace DonationManagement.Api.Services
{
    public class CaseService : ICaseService
    {
        private readonly DonationDbContext _context;

        public CaseService(DonationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CaseResponse>> GetAllCasesAsync()
        {
            return await _context.Cases
                .Include(c => c.Donor)
                .Include(c => c.Category)
                .Include(c => c.Supervisor)
                .Select(c => new CaseResponse(c.Id, c.Amount, c.Description, c.Status, c.Date, c.SupervisorId, c.DonorId, c.CategoryId))
                .ToListAsync();
        }

        public async Task<CaseResponse?> GetCaseByIdAsync(int id)
        {
            var caseEntity = await _context.Cases
                .Include(c => c.Donor)
                .Include(c => c.Category)
                .Include(c => c.Supervisor)
                .FirstOrDefaultAsync(c => c.Id == id);

            return caseEntity == null ? null :
                new CaseResponse(caseEntity.Id, caseEntity.Amount, caseEntity.Description, caseEntity.Status, caseEntity.Date, caseEntity.SupervisorId, caseEntity.DonorId, caseEntity.CategoryId);
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

            _context.Cases.Add(caseEntity);
            await _context.SaveChangesAsync();

            return new CaseResponse(caseEntity.Id, caseEntity.Amount, caseEntity.Description, caseEntity.Status, caseEntity.Date, caseEntity.SupervisorId, caseEntity.DonorId, caseEntity.CategoryId);
        }

        public async Task<CaseResponse?> UpdateCaseAsync(int id, CaseRequest request)
        {
            var caseEntity = await _context.Cases.FindAsync(id);
            if (caseEntity == null) return null;

            caseEntity.Amount = request.Amount;
            caseEntity.Description = request.Description;
            caseEntity.Status = request.Status;
            caseEntity.Date = request.Date;
            caseEntity.SupervisorId = request.SupervisorId;
            caseEntity.DonorId = request.DonorId;
            caseEntity.CategoryId = request.CategoryId;

            await _context.SaveChangesAsync();

            return new CaseResponse(caseEntity.Id, caseEntity.Amount, caseEntity.Description, caseEntity.Status, caseEntity.Date, caseEntity.SupervisorId, caseEntity.DonorId, caseEntity.CategoryId);
        }

        public async Task<bool> DeleteCaseAsync(int id)
        {
            var caseEntity = await _context.Cases.FindAsync(id);
            if (caseEntity == null) return false;

            _context.Cases.Remove(caseEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DistributionResponse>> GetCaseDistributionsAsync(int caseId)
        {
            return await _context.Distributions
                .Where(d => d.CaseId == caseId)
                .Select(d => new DistributionResponse(d.Id, d.Amount, d.DistributionDate, d.Status, d.Recipient, d.CaseId, d.HandledByEmployeeId))
                .ToListAsync();
        }

        public async Task<decimal> GetRemainingAmountNeededAsync(int caseId)
        {
            var caseEntity = await _context.Cases.FindAsync(caseId);
            if (caseEntity == null) return 0;

            var totalDistributed = await _context.Distributions
                .Where(d => d.CaseId == caseId && d.Status == "Completed")
                .SumAsync(d => d.Amount);

            return Math.Max(0, caseEntity.Amount - totalDistributed);
        }

        public async Task<bool> IsFullyFundedAsync(int caseId)
        {
            var remaining = await GetRemainingAmountNeededAsync(caseId);
            return remaining == 0;
        }
    }
}
