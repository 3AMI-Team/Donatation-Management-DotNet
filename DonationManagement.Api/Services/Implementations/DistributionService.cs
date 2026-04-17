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
        private readonly IDonationRepository _donationRepo;

        public DistributionService(IDistributionRepository distributionRepo, IDonationRepository donationRepo)
        {
            _distributionRepo = distributionRepo;
            _donationRepo = donationRepo;
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

        public async Task<DistributionKpis> GetDistributionKpisAsync()
        {
            var distributions = await _distributionRepo.GetAllAsync();
            var distList = distributions.ToList();

            if (!distList.Any())
            {
                var allDonations = await _donationRepo.GetAllAsync();
                var totalBalance = allDonations.Where(d => d.Status == "Completed").Sum(d => d.Amount);
                return new DistributionKpis(0, 0, 0, totalBalance);
            }

            var totalDistributed = distList.Sum(d => d.Amount);
            var casesServed = distList.Select(d => d.CaseId).Distinct().Count();
            var avgDistribution = totalDistributed / distList.Count;
            
            var completedDonations = await _donationRepo.FindAsync(d => d.Status == "Completed");
            var totalFund = completedDonations.Sum(d => d.Amount);
            var remainingBalance = totalFund - totalDistributed;

            return new DistributionKpis(totalDistributed, casesServed, avgDistribution, remainingBalance);
        }
    }
}
