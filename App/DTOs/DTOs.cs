using System;

namespace DonationManagement.Api.DTOs
{
    public record EmployeeRequest(string Phone, string Address, string Email, string Name, string Role, string Password, string Username);
    public record EmployeeResponse(int Id, string Phone, string Address, string Email, string Name, string Role, string Username);

    public record DonorRequest(string Name, string Email, string Phone);
    public record DonorResponse(int Id, string Name, string Email, string Phone, DateTime RegisterDate);

    public record CategoryRequest(string Type, string Description);
    public record CategoryResponse(int Id, string Type, string Description);

    public record CaseRequest(decimal Amount, string Description, string Status, DateTime Date, int? SupervisorId, int DonorId, int CategoryId);
    public record CaseResponse(int Id, decimal Amount, string Description, string Status, DateTime Date, int? SupervisorId, int DonorId, int CategoryId);

    public record DistributionRequest(decimal Amount, DateTime DistributionDate, string Status, string Recipient, int CaseId, int? HandledByEmployeeId);
    public record DistributionResponse(int Id, decimal Amount, DateTime DistributionDate, string Status, string Recipient, int CaseId, int? HandledByEmployeeId);

    public record EvenDistributionRequest(decimal TotalAmount, bool AutoDistribute);
}
