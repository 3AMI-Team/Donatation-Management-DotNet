namespace DonationManagement.Api.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeResponse>> GetAllEmployeesAsync();
        Task<PaginatedResponse<EmployeeResponse>> GetEmployeesPagedAsync(int page, int pageSize);
        Task<EmployeeResponse?> GetEmployeeByIdAsync(int id);
        Task<EmployeeResponse> CreateEmployeeAsync(EmployeeRequest request);
        Task<EmployeeResponse?> UpdateEmployeeAsync(int id, EmployeeRequest request);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<IEnumerable<CaseResponse>> GetRegisteredCasesAsync(int employeeId);
        Task<IEnumerable<DistributionResponse>> GetHandledDistributionsAsync(int employeeId);
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        // Employee management functions
        Task<DonorResponse> CreateDonorAsync(DonorRequest request);
        Task<bool> DeleteDonorAsync(int id);
        Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request);
        Task<bool> DeleteCategoryAsync(int id);
        Task<CaseResponse> CreateCaseAsync(CaseRequest request);
        Task<bool> DeleteCaseAsync(int id);
    }
}
