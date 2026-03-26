using DonationManagement.Api.DTOs;
using DonationManagement.Core.Entities;
using System.Linq;

namespace DonationManagement.Api.Mappings
{
    public static class MappingExtensions
    {
        // Employee Mappings
        public static EmployeeResponse ToResponse(this Employee employee) =>
            new EmployeeResponse(employee.Id, employee.Phone, employee.Address, employee.Email, employee.Name, employee.Role, employee.Username);

        // Donor Mappings
        public static DonorResponse ToResponse(this Donor donor) =>
            new DonorResponse(donor.Id, donor.Name, donor.Email, donor.Phone, donor.RegisterDate);

        // Category Mappings
        public static CategoryResponse ToResponse(this Category category) =>
            new CategoryResponse(category.Id, category.Type, category.Description);

        // Case Mappings
        public static CaseResponse ToResponse(this Case @case) =>
            new CaseResponse(@case.Id, @case.Amount, @case.Description, @case.Status, @case.Date, @case.SupervisorId, @case.DonorId, @case.CategoryId);

        // Distribution Mappings
        public static DistributionResponse ToResponse(this Distribution distribution) =>
            new DistributionResponse(distribution.Id, distribution.Amount, distribution.DistributionDate, distribution.Status, distribution.Recipient, distribution.CaseId, distribution.HandledByEmployeeId);
    }
}
