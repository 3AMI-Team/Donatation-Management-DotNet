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

        // Donation Mappings
        public static DonationResponse ToResponse(this Donation donation) =>
            new DonationResponse(
                donation.Id, 
                donation.Amount, 
                donation.Description, 
                donation.Status, 
                donation.Date, 
                donation.Donor?.Name ?? "Unknown", 
                donation.Category?.Type ?? "None", 
                donation.Supervisor?.Name ?? "Unassigned"
            );

        // Case Mappings
        public static CaseResponse ToResponse(this Case @case) =>
            new CaseResponse(
                @case.Id, 
                @case.Name, 
                @case.Phone, 
                @case.Address, 
                @case.RegistDate, 
                @case.Status, 
                @case.Description, 
                @case.Category?.Type ?? "None", 
                @case.Supervisor?.Name ?? "Unassigned"
            );

        // Distribution Mappings
        public static DistributionResponse ToResponse(this Distribution distribution) =>
            new DistributionResponse(
                distribution.Id, 
                distribution.Amount, 
                distribution.DistributionDate, 
                distribution.Status, 
                distribution.Notes, 
                distribution.CaseId, 
                distribution.Case?.Name ?? "Unknown", 
                distribution.DonationId, 
                distribution.Donation?.Donor?.Name ?? "Unknown", 
                distribution.HandledByEmployeeId, 
                distribution.HandledByEmployee?.Name ?? "System"
            );
    }
}
