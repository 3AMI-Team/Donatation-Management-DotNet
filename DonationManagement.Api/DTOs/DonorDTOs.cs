using System;

namespace DonationManagement.Api.DTOs
{
    public record DonorRequest(string Name, string Email, string Phone);
    public record DonorResponse(int Id, string Name, string Email, string Phone, DateTime RegisterDate);
}
