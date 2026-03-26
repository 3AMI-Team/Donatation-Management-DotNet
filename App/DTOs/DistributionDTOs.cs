using System;

namespace DonationManagement.Api.DTOs
{
    public record DistributionRequest(decimal Amount, DateTime DistributionDate, string Status, string Recipient, int CaseId, int? HandledByEmployeeId);
    public record DistributionResponse(int Id, decimal Amount, DateTime DistributionDate, string Status, string Recipient, int CaseId, int? HandledByEmployeeId);
}
