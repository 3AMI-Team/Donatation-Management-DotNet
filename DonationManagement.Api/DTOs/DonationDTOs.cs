using System;

namespace DonationManagement.Api.DTOs
{
    public record DonationRequest(decimal Amount, string Description, string Status, DateTime Date, int? SupervisorId, int DonorId, int CategoryId);
    
    public record DonationResponse(
        int Id, 
        decimal Amount, 
        string Description, 
        string Status, 
        DateTime Date, 
        string DonorName, 
        string CategoryName, 
        string SupervisorName
    );

    public record DonationKpis(decimal MonthlyTotal, int TransactionCount, string TopCategory, decimal PendingAmount);
}
