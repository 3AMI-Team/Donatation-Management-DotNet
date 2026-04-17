namespace DonationManagement.Api.DTOs
{
    public record EmployeeRequest(string Phone, string Address, string Email, string Name, string Role, string Password, string Username);
    public record EmployeeResponse(int Id, string Phone, string Address, string Email, string Name, string Role, string Username);
    public record EmployeeKpis(int TotalEmployees, int AdminCount, int ActiveSupervisors, double MonthlyActivityRate);
    public record EmployeePersonalKpis(decimal DonationsAmount, int CasesRegistered, int DistributionsManaged, string PerformanceScore);
}
