using System;

namespace DonationManagement.Api.DTOs
{
    public record CaseRequest(decimal Amount, string Description, string Status, DateTime Date, int? SupervisorId, int DonorId, int CategoryId);
    public record CaseResponse(int Id, decimal Amount, string Description, string Status, DateTime Date, int? SupervisorId, int DonorId, int CategoryId);
}
