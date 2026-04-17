using System;

namespace DonationManagement.Api.DTOs
{
    public record DistributionRequest(decimal Amount, DateTime DistributionDate, string Status, string Notes, int CaseId, int DonationId, int? HandledByEmployeeId);
    
    public record DistributionResponse(
        int Id, 
        decimal Amount, 
        DateTime DistributionDate, 
        string Status, 
        string Notes, 
        int CaseId, 
        string CaseName, 
        int DonationId, 
        string DonorName, 
        int? HandledByEmployeeId, 
        string HandledByEmployeeName
    );
    public record DistributionKpis(decimal TotalDistributed, int CasesServed, decimal AvgDistribution, decimal RemainingBalance);
}
