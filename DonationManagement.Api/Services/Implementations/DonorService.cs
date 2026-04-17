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
    public class DonorService : IDonorService
    {
        private readonly IDonorRepository _donorRepo;
        private readonly IDonationRepository _donationRepo; // Updated
        private readonly IJwtTokenService _jwtTokenService;

        public DonorService(IDonorRepository donorRepo, IDonationRepository donationRepo, IJwtTokenService jwtTokenService)
        {
            _donorRepo = donorRepo;
            _donationRepo = donationRepo;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<IEnumerable<DonorResponse>> GetAllDonorsAsync()
        {
            var donors = await _donorRepo.GetAllAsync();
            return donors.Select(d => d.ToResponse());
        }

        public async Task<PaginatedResponse<DonorResponse>> GetDonorsPagedAsync(int page, int pageSize)
        {
            var (normalizedPage, normalizedPageSize) = Pagination.Normalize(page, pageSize);
            var totalCount = await _donorRepo.CountAsync();

            var donors = await _donorRepo.GetPagedAsync(normalizedPage, normalizedPageSize);
            var items = donors.Select(d => d.ToResponse()).ToList();

            var totalPages = Pagination.GetTotalPages(totalCount, normalizedPageSize);
            return new PaginatedResponse<DonorResponse>(items, normalizedPage, normalizedPageSize, totalCount, totalPages);
        }

        public async Task<DonorResponse?> GetDonorByIdAsync(int id)
        {
            var donor = await _donorRepo.GetByIdAsync(id);
            return donor?.ToResponse();
        }

        public async Task<DonorResponse> CreateDonorAsync(DonorRequest request)
        {
            var donor = new Donor
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address, // Added from new schema
                Type = request.Type,       // Added from new schema
                RegisterDate = DateTime.UtcNow
            };

            await _donorRepo.AddAsync(donor);
            await _donorRepo.SaveChangesAsync();

            return donor.ToResponse();
        }

        public async Task<DonorResponse?> UpdateDonorAsync(int id, DonorRequest request)
        {
            var donor = await _donorRepo.GetByIdAsync(id);
            if (donor == null) return null;

            donor.Name = request.Name;
            donor.Email = request.Email;
            donor.Phone = request.Phone;
            donor.Address = request.Address;
            donor.Type = request.Type;

            _donorRepo.Update(donor);
            await _donorRepo.SaveChangesAsync();

            return donor.ToResponse();
        }

        public async Task<bool> DeleteDonorAsync(int id)
        {
            var donor = await _donorRepo.GetByIdAsync(id);
            if (donor == null) return false;

            _donorRepo.Remove(donor);
            await _donorRepo.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DonationResponse>> GetDonorDonationsAsync(int donorId)
        {
            var donations = await _donationRepo.FindAsync(d => d.DonorId == donorId);
            return donations.Select(d => d.ToResponse());
        }

        public async Task<DonorResponse> SignupAsync(DonorSignupRequest request)
        {
            var donor = new Donor
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RegisterDate = DateTime.UtcNow,
                Type = "Individual" // Default for signup
            };

            await _donorRepo.AddAsync(donor);
            await _donorRepo.SaveChangesAsync();

            return donor.ToResponse();
        }

        public async Task<AuthResponse?> LoginAsync(DonorLoginRequest request)
        {
            var donors = await _donorRepo.FindAsync(d => d.Email == request.Email);
            var donor = donors.FirstOrDefault();

            if (donor == null || string.IsNullOrEmpty(donor.Password)) return null;

            if (!BCrypt.Net.BCrypt.Verify(request.Password, donor.Password))
                return null;

            var token = _jwtTokenService.GenerateToken(donor.Email, "Donor");

            return new AuthResponse(donor.Id, donor.Name, donor.Email, "Donor", token);
        }
    }
}
