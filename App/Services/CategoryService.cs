using Microsoft.EntityFrameworkCore;
using DonationManagement.Api.DTOs;
using DonationManagement.Core;
using DonationManagement.Core.Entities;

namespace DonationManagement.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DonationDbContext _context;

        public CategoryService(DonationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryResponse(c.Id, c.Type, c.Description))
                .ToListAsync();
        }

        public async Task<PaginatedResponse<CategoryResponse>> GetCategoriesPagedAsync(int page, int pageSize)
        {
            var (normalizedPage, normalizedPageSize) = Pagination.Normalize(page, pageSize);
            var totalCount = await _context.Categories.CountAsync();

            var items = await _context.Categories
                .OrderBy(c => c.Id)
                .Skip((normalizedPage - 1) * normalizedPageSize)
                .Take(normalizedPageSize)
                .Select(c => new CategoryResponse(c.Id, c.Type, c.Description))
                .ToListAsync();

            var totalPages = Pagination.GetTotalPages(totalCount, normalizedPageSize);
            return new PaginatedResponse<CategoryResponse>(items, normalizedPage, normalizedPageSize, totalCount, totalPages);
        }

        public async Task<CategoryResponse?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category == null ? null :
                new CategoryResponse(category.Id, category.Type, category.Description);
        }

        public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request)
        {
            var category = new Category
            {
                Type = request.Type,
                Description = request.Description
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryResponse(category.Id, category.Type, category.Description);
        }

        public async Task<CategoryResponse?> UpdateCategoryAsync(int id, CategoryRequest request)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            category.Type = request.Type;
            category.Description = request.Description;

            await _context.SaveChangesAsync();

            return new CategoryResponse(category.Id, category.Type, category.Description);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CaseResponse>> GetCategoryCasesAsync(int categoryId)
        {
            return await _context.Cases
                .Where(c => c.CategoryId == categoryId)
                .Select(c => new CaseResponse(c.Id, c.Amount, c.Description, c.Status, c.Date, c.SupervisorId, c.DonorId, c.CategoryId))
                .ToListAsync();
        }
    }
}
