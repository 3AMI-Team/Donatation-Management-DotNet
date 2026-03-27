using System;

namespace DonationManagement.Api.Services
{
    internal static class Pagination
    {
        public const int DefaultPageSize = 20;
        public const int MaxPageSize = 100;

        public static (int Page, int PageSize) Normalize(int page, int pageSize)
        {
            var normalizedPage = page < 1 ? 1 : page;
            var normalizedPageSize = pageSize < 1 ? DefaultPageSize : pageSize;
            if (normalizedPageSize > MaxPageSize) normalizedPageSize = MaxPageSize;

            return (normalizedPage, normalizedPageSize);
        }

        public static int GetTotalPages(int totalCount, int pageSize)
        {
            if (pageSize <= 0) return 0;
            return (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
