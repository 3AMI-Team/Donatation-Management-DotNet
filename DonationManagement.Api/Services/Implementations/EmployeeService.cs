using DonationManagement.Api.DTOs;
using DonationManagement.Api.Mappings;
using DonationManagement.Api.Services;
using DonationManagement.Api.Services.Interfaces;
using DonationManagement.Core;
using DonationManagement.Core.Entities;
using DonationManagement.Core.Repositories.Interfaces;
using BCrypt.Net;

namespace DonationManagement.Api.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IDonorRepository _donorRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ICaseRepository _caseRepo;
        private readonly IDistributionRepository _distributionRepo;
        private readonly IJwtTokenService _jwtTokenService;

        public EmployeeService(
            IEmployeeRepository employeeRepo,
            IDonorRepository donorRepo,
            ICategoryRepository categoryRepo,
            ICaseRepository caseRepo,
            IDistributionRepository distributionRepo,
            IJwtTokenService jwtTokenService)
        {
            _employeeRepo = employeeRepo;
            _donorRepo = donorRepo;
            _categoryRepo = categoryRepo;
            _caseRepo = caseRepo;
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

        public async Task<IEnumerable<DistributionResponse>> GetHandledDistributionsAsync(int employeeId)
        {
            var distributions = await _distributionRepo.FindAsync(d => d.HandledByEmployeeId == employeeId);
            return distributions.Select(d => d.ToResponse());
        }

        // Employee management functions for donors, categories, cases
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
                Amount = request.Amount,
                Description = request.Description,
                Status = request.Status,
                Date = request.Date,
                SupervisorId = request.SupervisorId,
                DonorId = request.DonorId,
                CategoryId = request.CategoryId
            };

            await _caseRepo.AddAsync(caseEntity);
            await _caseRepo.SaveChangesAsync();

            return caseEntity.ToResponse();
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
            var employees = await _employeeRepo.FindAsync(e => e.Username == request.Username);
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
    }
}
