namespace DonationManagement.Api.DTOs
{
    public record LoginRequest(string Username, string Password);
    public record DonorLoginRequest(string Email, string Password);
    
    public record AuthResponse(
        int Id,
        string Name,
        string Username,
        string Role,
        string? Token = null // For future JWT integration
    );
}
