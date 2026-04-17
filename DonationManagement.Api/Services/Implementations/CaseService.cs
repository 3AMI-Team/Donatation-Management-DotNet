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
    public class CaseService : ICaseService
    {
        private readonly ICaseRepository _caseRepo;

        public CaseService(ICaseRepository caseRepo)
        {
            _caseRepo = caseRepo;
        }

        public async Task<IEnumerable<CaseResponse>> GetAllCasesAsync(int? categoryId = null)
        {
            IEnumerable<Case> cases;
            if (categoryId.HasValue)
            {
                cases = await _caseRepo.FindAsync(c => c.CategoryId == categoryId.Value);
            }
            else
            {
                cases = await _caseRepo.GetAllAsync();
            }
            return cases.Select(c => c.ToResponse());
        }

        public async Task<PaginatedResponse<CaseResponse>> GetCasesPagedAsync(int page, int pageSize, int? categoryId = null)
        {
            var (normalizedPage, normalizedPageSize) = Pagination.Normalize(page, pageSize);
            
            IEnumerable<Case> cases;
            int totalCount;

            if (categoryId.HasValue)
            {
                cases = await _caseRepo.FindAsync(c => c.CategoryId == categoryId.Value);
                totalCount = cases.Count();
                cases = cases.Skip((normalizedPage - 1) * normalizedPageSize).Take(normalizedPageSize);
            }
            else
            {
                totalCount = await _caseRepo.CountAsync();
                cases = await _caseRepo.GetPagedAsync(normalizedPage, normalizedPageSize);
            }

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
                Name = request.Name,
                Phone = request.Phone,
                Address = request.Address,
                RegistDate = request.RegistDate,
                Status = request.Status,
                Description = request.Description,
                CategoryId = request.CategoryId,
                SupervisorId = request.SupervisorId
            };

            await _caseRepo.AddAsync(caseEntity);
            await _caseRepo.SaveChangesAsync();

            return (await _caseRepo.GetByIdAsync(caseEntity.Id))!.ToResponse();
        }

        public async Task<CaseResponse?> UpdateCaseAsync(int id, CaseRequest request)
        {
            var caseEntity = await _caseRepo.GetByIdAsync(id);
            if (caseEntity == null) return null;

            caseEntity.Name = request.Name;
            caseEntity.Phone = request.Phone;
            caseEntity.Address = request.Address;
            caseEntity.RegistDate = request.RegistDate;
            caseEntity.Status = request.Status;
            caseEntity.Description = request.Description;
            caseEntity.CategoryId = request.CategoryId;
            caseEntity.SupervisorId = request.SupervisorId;

            _caseRepo.Update(caseEntity);
            await _caseRepo.SaveChangesAsync();

            return (await _caseRepo.GetByIdAsync(id))!.ToResponse();
        }

        public async Task<bool> DeleteCaseAsync(int id)
        {
            var caseEntity = await _caseRepo.GetByIdAsync(id);
            if (caseEntity == null) return false;

            _caseRepo.Remove(caseEntity);
            await _caseRepo.SaveChangesAsync();
            return true;
        }
    }
}
