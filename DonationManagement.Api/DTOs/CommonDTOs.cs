using System.Collections.Generic;

namespace DonationManagement.Api.DTOs
{
    public record PaginatedResponse<T>(IReadOnlyList<T> Items, int Page, int PageSize, int TotalCount, int TotalPages);
    public record EvenDistributionRequest(decimal TotalAmount, bool AutoDistribute);
}
