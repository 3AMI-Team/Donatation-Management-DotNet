using DonationManagement.Api.DTOs;
using DonationManagement.Core;
using DonationManagement.Core.Entities;
using DonationManagement.Core.Repositories;

namespace DonationManagement.Api.Services
{
    public class DonorService : IDonorService
    {
        private readonly IDonorRepository _donorRepo;
        private readonly ICaseRepository _caseRepo;

        public DonorService(IDonorRepository donorRepo, ICaseRepository caseRepo)
        {
            _donorRepo = donorRepo;
            _caseRepo = caseRepo;
        }

        public async Task<IEnumerable<DonorResponse>> GetAllDonorsAsync()
        {
            var donors = await _donorRepo.GetAllAsync();
            return donors.Select(d => new DonorResponse(d.Id, d.Name, d.Email, d.Phone, d.RegisterDate));
        }

        public async Task<PaginatedResponse<DonorResponse>> GetDonorsPagedAsync(int page, int pageSize)
        {
            var (normalizedPage, normalizedPageSize) = Pagination.Normalize(page, pageSize);
            var totalCount = await _donorRepo.CountAsync();

            var donors = await _donorRepo.GetAllAsync();
            var items = donors
                .OrderBy(d => d.Id)
                .Skip((normalizedPage - 1) * normalizedPageSize)
                .Take(normalizedPageSize)
                .Select(d => new DonorResponse(d.Id, d.Name, d.Email, d.Phone, d.RegisterDate))
                .ToList();

            var totalPages = Pagination.GetTotalPages(totalCount, normalizedPageSize);
            return new PaginatedResponse<DonorResponse>(items, normalizedPage, normalizedPageSize, totalCount, totalPages);
        }

        public async Task<DonorResponse?> GetDonorByIdAsync(int id)
        {
            var donor = await _donorRepo.GetByIdAsync(id);
            return donor == null ? null :
                new DonorResponse(donor.Id, donor.Name, donor.Email, donor.Phone, donor.RegisterDate);
        }

        public async Task<DonorResponse> CreateDonorAsync(DonorRequest request)
        {
            var donor = new Donor
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                RegisterDate = DateTime.UtcNow
            };

            await _donorRepo.AddAsync(donor);
            await _donorRepo.SaveChangesAsync();

            return new DonorResponse(donor.Id, donor.Name, donor.Email, donor.Phone, donor.RegisterDate);
        }

        public async Task<DonorResponse?> UpdateDonorAsync(int id, DonorRequest request)
        {
            var donor = await _donorRepo.GetByIdAsync(id);
            if (donor == null) return null;

            donor.Name = request.Name;
            donor.Email = request.Email;
            donor.Phone = request.Phone;

            _donorRepo.Update(donor);
            await _donorRepo.SaveChangesAsync();

            return new DonorResponse(donor.Id, donor.Name, donor.Email, donor.Phone, donor.RegisterDate);
        }

        public async Task<bool> DeleteDonorAsync(int id)
        {
            var donor = await _donorRepo.GetByIdAsync(id);
            if (donor == null) return false;

            _donorRepo.Remove(donor);
            await _donorRepo.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CaseResponse>> GetDonorCasesAsync(int donorId)
        {
            var cases = await _caseRepo.FindAsync(c => c.DonorId == donorId);
            return cases.Select(c => new CaseResponse(c.Id, c.Amount, c.Description, c.Status, c.Date, c.SupervisorId, c.DonorId, c.CategoryId));
        }
    }
}
