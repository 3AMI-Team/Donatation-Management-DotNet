namespace DonationManagement.Api.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync();
        Task<PaginatedResponse<CategoryResponse>> GetCategoriesPagedAsync(int page, int pageSize);
        Task<CategoryResponse?> GetCategoryByIdAsync(int id);
        Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request);
        Task<CategoryResponse?> UpdateCategoryAsync(int id, CategoryRequest request);
        Task<bool> DeleteCategoryAsync(int id);
        Task<IEnumerable<CaseResponse>> GetCategoryCasesAsync(int categoryId);
    }
}
