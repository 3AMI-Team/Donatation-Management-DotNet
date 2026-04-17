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
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IDonorRepository _donorRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ICaseRepository _caseRepo;
        private readonly IDonationRepository _donationRepo; // NEW
        private readonly IDistributionRepository _distributionRepo;
        private readonly IJwtTokenService _jwtTokenService;

        public EmployeeService(
            IEmployeeRepository employeeRepo,
            IDonorRepository donorRepo,
            ICategoryRepository categoryRepo,
            ICaseRepository caseRepo,
            IDonationRepository donationRepo,
            IDistributionRepository distributionRepo,
            IJwtTokenService jwtTokenService)
        {
            _employeeRepo = employeeRepo;
            _donorRepo = donorRepo;
            _categoryRepo = categoryRepo;
            _caseRepo = caseRepo;
            _donationRepo = donationRepo;
            _distributionRepo = distributionRepo;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<IEnumerable<EmployeeResponse>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepo.GetAllAsync();
            return employees.Select(e => e.ToResponse());
        }

        public async Task<PaginatedResponse<EmployeeResponse>> GetEmployeesPagedAsync(int page, int pageSize)
        {
            var (normalizedPage, normalizedPageSize) = Pagination.Normalize(page, pageSize);
            var totalCount = await _employeeRepo.CountAsync();

            var employees = await _employeeRepo.GetPagedAsync(normalizedPage, normalizedPageSize);
            var items = employees.Select(e => e.ToResponse()).ToList();

            var totalPages = Pagination.GetTotalPages(totalCount, normalizedPageSize);
            return new PaginatedResponse<EmployeeResponse>(items, normalizedPage, normalizedPageSize, totalCount, totalPages);
        }

        public async Task<EmployeeResponse?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepo.GetByIdAsync(id);
            return employee?.ToResponse();
        }

        public async Task<EmployeeResponse> CreateEmployeeAsync(EmployeeRequest request)
        {
            var employee = new Employee
            {
                Phone = request.Phone,
                Address = request.Address,
                Email = request.Email,
                Name = request.Name,
                Role = request.Role,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Username = request.Username
            };

            await _employeeRepo.AddAsync(employee);
            await _employeeRepo.SaveChangesAsync();

            return employee.ToResponse();
        }

        public async Task<EmployeeResponse?> UpdateEmployeeAsync(int id, EmployeeRequest request)
        {
            var employee = await _employeeRepo.GetByIdAsync(id);
            if (employee == null) return null;

            employee.Phone = request.Phone;
            employee.Address = request.Address;
            employee.Email = request.Email;
            employee.Name = request.Name;
            employee.Role = request.Role;
            employee.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            employee.Username = request.Username;

            _employeeRepo.Update(employee);
            await _employeeRepo.SaveChangesAsync();

            return employee.ToResponse();
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _employeeRepo.GetByIdAsync(id);
            if (employee == null) return false;

            _employeeRepo.Remove(employee);
            await _employeeRepo.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CaseResponse>> GetRegisteredCasesAsync(int employeeId)
        {
            var cases = await _caseRepo.FindAsync(c => c.SupervisorId == employeeId);
            return cases.Select(c => c.ToResponse());
        }

        public async Task<IEnumerable<DonationResponse>> GetRegisteredDonationsAsync(int employeeId)
        {
            var donations = await _donationRepo.FindAsync(d => d.SupervisorId == employeeId);
            return donations.Select(d => d.ToResponse());
        }

        public async Task<IEnumerable<DistributionResponse>> GetHandledDistributionsAsync(int employeeId)
        {
            var distributions = await _distributionRepo.FindAsync(d => d.HandledByEmployeeId == employeeId);
            return distributions.Select(d => d.ToResponse());
        }

        public async Task<DonorResponse> CreateDonorAsync(DonorRequest request)
        {
            var donor = new Donor
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                Type = request.Type,
                RegisterDate = DateTime.UtcNow
            };

            await _donorRepo.AddAsync(donor);
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

        public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request)
        {
            var category = new Category
            {
                Type = request.Type,
                Description = request.Description
            };

            await _categoryRepo.AddAsync(category);
            await _categoryRepo.SaveChangesAsync();

            return category.ToResponse();
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null) return false;

            _categoryRepo.Remove(category);
            await _categoryRepo.SaveChangesAsync();
            return true;
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

        public async Task<bool> DeleteCaseAsync(int id)
        {
            var caseEntity = await _caseRepo.GetByIdAsync(id);
            if (caseEntity == null) return false;

            _caseRepo.Remove(caseEntity);
            await _caseRepo.SaveChangesAsync();
            return true;
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            var username = request.Username.Trim().ToLower();
            var employees = await _employeeRepo.FindAsync(e => e.Username.ToLower() == username);
            var employee = employees.FirstOrDefault();

            if (employee == null || !BCrypt.Net.BCrypt.Verify(request.Password, employee.Password))
            {
                return null;
            }

            var token = _jwtTokenService.GenerateToken(employee.Username, employee.Role);

            return new AuthResponse(
                employee.Id,
                employee.Name,
                employee.Username,
                employee.Role,
                token
            );
        }

        public async Task<EmployeeKpis> GetEmployeeKpisAsync()
        {
            var employees = await _employeeRepo.GetAllAsync();
            var employeeList = employees.ToList();

            if (!employeeList.Any())
                return new EmployeeKpis(0, 0, 0, 0);

            var totalEmployees = employeeList.Count;
            var adminCount = employeeList.Count(e => e.Role == "Admin");
            var activeSupervisors = employeeList.Count(e => e.Role == "Supervisor");
            
            var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);
            
            var activeDonations = await _donationRepo.FindAsync(d => d.Date >= thirtyDaysAgo);
            var activeCases = await _caseRepo.FindAsync(c => c.RegistDate >= thirtyDaysAgo);
            var activeDistributions = await _distributionRepo.FindAsync(d => d.DistributionDate >= thirtyDaysAgo);
            
            var activeEmployeeIds = activeDonations.Select(d => d.SupervisorId)
                .Concat(activeCases.Select(c => c.SupervisorId))
                .Concat(activeDistributions.Select(d => d.HandledByEmployeeId))
                .Where(id => id.HasValue)
                .Select(id => id!.Value)
                .Distinct()
                .ToList();

            var monthlyActivityRate = (double)activeEmployeeIds.Count / totalEmployees;

            return new EmployeeKpis(totalEmployees, adminCount, activeSupervisors, monthlyActivityRate);
        }

        public async Task<EmployeePersonalKpis> GetEmployeePersonalKpisAsync(int employeeId)
        {
            var donations = await _donationRepo.FindAsync(d => d.SupervisorId == employeeId);
            var cases = await _caseRepo.FindAsync(c => c.SupervisorId == employeeId);
            var distributions = await _distributionRepo.FindAsync(d => d.HandledByEmployeeId == employeeId);

            var donationsAmount = donations.Sum(d => d.Amount);
            var casesCount = cases.Count();
            var distCount = distributions.Count();
            
            var score = (donations.Count() + casesCount + distCount);
            var performanceScore = score > 20 ? "High" : score > 5 ? "Medium" : "Low";

            return new EmployeePersonalKpis(donationsAmount, casesCount, distCount, performanceScore);
        }
    }
}
