namespace DonationManagement.Api.DTOs
{
    public record CategoryRequest(string Type, string Description);
    public record CategoryResponse(int Id, string Type, string Description);
}
