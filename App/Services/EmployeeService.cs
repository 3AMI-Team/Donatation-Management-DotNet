using Microsoft.EntityFrameworkCore;
using DonationManagement.Api.DTOs;
using DonationManagement.Core;
using DonationManagement.Core.Entities;

namespace DonationManagement.Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DonationDbContext _context;

        public EmployeeService(DonationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeResponse>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                .Select(e => new EmployeeResponse(e.Id, e.Phone, e.Address, e.Email, e.Name, e.Role, e.Username))
                .ToListAsync();
        }

        public async Task<EmployeeResponse?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            return employee == null ? null :
                new EmployeeResponse(employee.Id, employee.Phone, employee.Address, employee.Email, employee.Name, employee.Role, employee.Username);
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
                Password = request.Password, // In real app, hash this
                Username = request.Username
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return new EmployeeResponse(employee.Id, employee.Phone, employee.Address, employee.Email, employee.Name, employee.Role, employee.Username);
        }

        public async Task<EmployeeResponse?> UpdateEmployeeAsync(int id, EmployeeRequest request)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return null;

            employee.Phone = request.Phone;
            employee.Address = request.Address;
            employee.Email = request.Email;
            employee.Name = request.Name;
            employee.Role = request.Role;
            employee.Password = request.Password; // In real app, hash this
            employee.Username = request.Username;

            await _context.SaveChangesAsync();

            return new EmployeeResponse(employee.Id, employee.Phone, employee.Address, employee.Email, employee.Name, employee.Role, employee.Username);
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CaseResponse>> GetRegisteredCasesAsync(int employeeId)
        {
            return await _context.Cases
                .Where(c => c.SupervisorId == employeeId)
                .Select(c => new CaseResponse(c.Id, c.Amount, c.Description, c.Status, c.Date, c.SupervisorId, c.DonorId, c.CategoryId))
                .ToListAsync();
        }

        public async Task<IEnumerable<DistributionResponse>> GetHandledDistributionsAsync(int employeeId)
        {
            return await _context.Distributions
                .Where(d => d.HandledByEmployeeId == employeeId)
                .Select(d => new DistributionResponse(d.Id, d.Amount, d.DistributionDate, d.Status, d.Recipient, d.CaseId, d.HandledByEmployeeId))
                .ToListAsync();
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

            _context.Donors.Add(donor);
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

        public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request)
        {
            var category = new Category
            {
                Type = request.Type,
                Description = request.Description
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryResponse(category.Id, category.Type, category.Description);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
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

            _context.Cases.Add(caseEntity);
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
    }
}
