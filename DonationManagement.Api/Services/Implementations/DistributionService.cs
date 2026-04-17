using DonationManagement.Api.DTOs;
using DonationManagement.Api.Mappings;
using DonationManagement.Api.Services;
using DonationManagement.Api.Services.Interfaces;
using DonationManagement.Core;
using DonationManagement.Core.Entities;
using DonationManagement.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonationManagement.Api.Services.Implementations
{
    public class DistributionService : IDistributionService
    {
        private readonly IDistributionRepository _distributionRepo;

        public DistributionService(IDistributionRepository distributionRepo)
        {
            _distributionRepo = distributionRepo;
        }

        public async Task<IEnumerable<DistributionResponse>> GetAllDistributionsAsync()
        {
            var distributions = await _distributionRepo.GetAllAsync();
            return distributions.Select(d => d.ToResponse());
        }

        public async Task<DistributionResponse?> GetDistributionByIdAsync(int id)
        {
            var distribution = await _distributionRepo.GetByIdAsync(id);
            return distribution?.ToResponse();
        }

        public async Task<DistributionResponse> CreateDistributionAsync(DistributionRequest request)
        {
            var distribution = new Distribution
            {
                Amount = request.Amount,
                DistributionDate = request.DistributionDate,
                Status = request.Status,
                Notes = request.Notes,
                CaseId = request.CaseId,
                DonationId = request.DonationId,
                HandledByEmployeeId = request.HandledByEmployeeId
            };

            await _distributionRepo.AddAsync(distribution);
            await _distributionRepo.SaveChangesAsync();

            // Fetch again to ensure navigation properties are loaded
            return (await _distributionRepo.GetByIdAsync(distribution.Id))!.ToResponse();
        }

        public async Task<DistributionResponse?> UpdateDistributionAsync(int id, DistributionRequest request)
        {
            var distribution = await _distributionRepo.GetByIdAsync(id);
            if (distribution == null) return null;

            distribution.Amount = request.Amount;
            distribution.DistributionDate = request.DistributionDate;
            distribution.Status = request.Status;
            distribution.Notes = request.Notes;
            distribution.CaseId = request.CaseId;
            distribution.DonationId = request.DonationId;
            distribution.HandledByEmployeeId = request.HandledByEmployeeId;

            _distributionRepo.Update(distribution);
            await _distributionRepo.SaveChangesAsync();

            return (await _distributionRepo.GetByIdAsync(id))!.ToResponse();
        }

        public async Task<bool> DeleteDistributionAsync(int id)
        {
            var distribution = await _distributionRepo.GetByIdAsync(id);
            if (distribution == null) return false;

            _distributionRepo.Remove(distribution);
            await _distributionRepo.SaveChangesAsync();
            return true;
        }
    }
}
