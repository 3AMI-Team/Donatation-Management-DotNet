using System;

namespace DonationManagement.Api.DTOs
{
    public record CaseRequest(string Name, string Phone, string Address, DateTime RegistDate, string Status, string Description, int CategoryId, int? SupervisorId);
    
    public record CaseResponse(
        int Id, 
        string Name, 
        string Phone, 
        string Address, 
        DateTime RegistDate, 
        string Status, 
        string Description, 
        string CategoryName, 
        string SupervisorName
    );
}
