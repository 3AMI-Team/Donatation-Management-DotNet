using DonationManagement.Api.DTOs;
using DonationManagement.Api.Mappings;
using DonationManagement.Api.Services;
using DonationManagement.Api.Services.Interfaces;
using DonationManagement.Core;
using DonationManagement.Core.Entities;
using DonationManagement.Core.Repositories.Interfaces;

namespace DonationManagement.Api.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly ICaseRepository _caseRepo;

        public CategoryService(ICategoryRepository categoryRepo, ICaseRepository caseRepo)
        {
            _categoryRepo = categoryRepo;
            _caseRepo = caseRepo;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return categories.Select(c => c.ToResponse());
        }

        public async Task<PaginatedResponse<CategoryResponse>> GetCategoriesPagedAsync(int page, int pageSize)
        {
            var (normalizedPage, normalizedPageSize) = Pagination.Normalize(page, pageSize);
            var totalCount = await _categoryRepo.CountAsync();

            var categories = await _categoryRepo.GetPagedAsync(normalizedPage, normalizedPageSize);
            var items = categories.Select(c => c.ToResponse()).ToList();

            var totalPages = Pagination.GetTotalPages(totalCount, normalizedPageSize);
            return new PaginatedResponse<CategoryResponse>(items, normalizedPage, normalizedPageSize, totalCount, totalPages);
        }

        public async Task<CategoryResponse?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            return category?.ToResponse();
        }

        public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request)
        {
            var category = new Category
            {
                Type = request.Type,
                Description = request.Description
            };

            await _categoryRepo.AddAsync(category);
            await _categoryRepo.SaveChangesAsync();

            return category.ToResponse();
        }

        public async Task<CategoryResponse?> UpdateCategoryAsync(int id, CategoryRequest request)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null) return null;

            category.Type = request.Type;
            category.Description = request.Description;

            _categoryRepo.Update(category);
            await _categoryRepo.SaveChangesAsync();

            return category.ToResponse();
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null) return false;

            _categoryRepo.Remove(category);
            await _categoryRepo.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CaseResponse>> GetCategoryCasesAsync(int categoryId)
        {
            var cases = await _caseRepo.FindAsync(c => c.CategoryId == categoryId);
            return cases.Select(c => c.ToResponse());
        }
    }
}
