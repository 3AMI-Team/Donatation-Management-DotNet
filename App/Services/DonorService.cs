using Microsoft.EntityFrameworkCore;
using DonationManagement.Api.DTOs;
using DonationManagement.Core;
using DonationManagement.Core.Entities;

namespace DonationManagement.Api.Services
{
    public class DonorService : IDonorService
    {
        private readonly DonationDbContext _context;

        public DonorService(DonationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DonorResponse>> GetAllDonorsAsync()
        {
            return await _context.Donors
                .Select(d => new DonorResponse(d.Id, d.Name, d.Email, d.Phone, d.RegisterDate))
                .ToListAsync();
        }

        public async Task<DonorResponse?> GetDonorByIdAsync(int id)
        {
            var donor = await _context.Donors.FindAsync(id);
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

            _context.Donors.Add(donor);
            await _context.SaveChangesAsync();

            return new DonorResponse(donor.Id, donor.Name, donor.Email, donor.Phone, donor.RegisterDate);
        }

        public async Task<DonorResponse?> UpdateDonorAsync(int id, DonorRequest request)
        {
            var donor = await _context.Donors.FindAsync(id);
            if (donor == null) return null;

            donor.Name = request.Name;
            donor.Email = request.Email;
            donor.Phone = request.Phone;

            await _context.SaveChangesAsync();

            return new DonorResponse(donor.Id, donor.Name, donor.Email, donor.Phone, donor.RegisterDate);
        }

        public async Task<bool> DeleteDonorAsync(int id)
        {
            var donor = await _context.Donors.FindAsync(id);
            if (donor == null) return false;

            _context.Donors.Remove(donor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CaseResponse>> GetDonorCasesAsync(int donorId)
        {
            return await _context.Cases
                .Where(c => c.DonorId == donorId)
                .Select(c => new CaseResponse(c.Id, c.Amount, c.Description, c.Status, c.Date, c.SupervisorId, c.DonorId, c.CategoryId))
                .ToListAsync();
        }
    }
}
