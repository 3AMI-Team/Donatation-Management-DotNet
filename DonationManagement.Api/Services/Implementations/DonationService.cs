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
    public class DonationService : IDonationService
    {
        private readonly IDonationRepository _donationRepo;

        public DonationService(IDonationRepository donationRepo)
        {
            _donationRepo = donationRepo;
        }

        public async Task<IEnumerable<DonationResponse>> GetAllDonationsAsync(int? categoryId = null)
        {
            IEnumerable<Donation> donations;
            if (categoryId.HasValue)
            {
                donations = await _donationRepo.FindAsync(d => d.CategoryId == categoryId.Value);
            }
            else
            {
                donations = await _donationRepo.GetAllAsync();
            }
            return donations.Select(d => d.ToResponse());
        }

        public async Task<PaginatedResponse<DonationResponse>> GetDonationsPagedAsync(int page, int pageSize, int? categoryId = null)
        {
            var (normalizedPage, normalizedPageSize) = Pagination.Normalize(page, pageSize);
            
            IEnumerable<Donation> donations;
            int totalCount;

            if (categoryId.HasValue)
            {
                donations = await _donationRepo.FindAsync(d => d.CategoryId == categoryId.Value);
                totalCount = donations.Count();
                donations = donations.Skip((normalizedPage - 1) * normalizedPageSize).Take(normalizedPageSize);
            }
            else
            {
                totalCount = await _donationRepo.CountAsync();
                donations = await _donationRepo.GetPagedAsync(normalizedPage, normalizedPageSize);
            }

            var items = donations.Select(d => d.ToResponse()).ToList();
            var totalPages = Pagination.GetTotalPages(totalCount, normalizedPageSize);
            
            return new PaginatedResponse<DonationResponse>(items, normalizedPage, normalizedPageSize, totalCount, totalPages);
        }

        public async Task<DonationResponse?> GetDonationByIdAsync(int id)
        {
            var donation = await _donationRepo.GetByIdAsync(id);
            return donation?.ToResponse();
        }

        public async Task<DonationResponse> CreateDonationAsync(DonationRequest request)
        {
            var donation = new Donation
            {
                Amount = request.Amount,
                Description = request.Description,
                Status = request.Status,
                Date = request.Date,
                SupervisorId = request.SupervisorId,
                DonorId = request.DonorId,
                CategoryId = request.CategoryId
            };

            await _donationRepo.AddAsync(donation);
            await _donationRepo.SaveChangesAsync();

            // Fetch again to include navigation properties for the response
            return (await _donationRepo.GetByIdAsync(donation.Id))!.ToResponse();
        }

        public async Task<DonationResponse?> UpdateDonationAsync(int id, DonationRequest request)
        {
            var donation = await _donationRepo.GetByIdAsync(id);
            if (donation == null) return null;

            donation.Amount = request.Amount;
            donation.Description = request.Description;
            donation.Status = request.Status;
            donation.Date = request.Date;
            donation.SupervisorId = request.SupervisorId;
            donation.DonorId = request.DonorId;
            donation.CategoryId = request.CategoryId;

            _donationRepo.Update(donation);
            await _donationRepo.SaveChangesAsync();

            return (await _donationRepo.GetByIdAsync(id))!.ToResponse();
        }

        public async Task<bool> DeleteDonationAsync(int id)
        {
            var donation = await _donationRepo.GetByIdAsync(id);
            if (donation == null) return false;

            _donationRepo.Remove(donation);
            await _donationRepo.SaveChangesAsync();
            return true;
        }

        public async Task<DonationKpis> GetDonationKpisAsync()
        {
            var donations = await _donationRepo.GetAllAsync();
            var donationList = donations.ToList();

            if (!donationList.Any())
                return new DonationKpis(0, 0, 0, 0);

            var totalAmount = donationList.Sum(d => d.Amount);
            var completedCount = donationList.Count(d => d.Status == "Completed");
            var pendingCount = donationList.Count(d => d.Status == "Pending");
            var avgAmount = totalAmount / donationList.Count;

            return new DonationKpis(totalAmount, completedCount, pendingCount, avgAmount);
        }
    }
}
