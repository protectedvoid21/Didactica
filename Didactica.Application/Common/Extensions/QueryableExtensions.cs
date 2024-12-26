using Didactica.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Didactica.Application.Common.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginationResponse<T>> ToPaginationResponseAsync<T>(this IQueryable<T> query, int page, int pageSize)
    {
        return await ToPaginationResponseAsync(query, new PaginationRequest { Page = page, PageSize = pageSize });
    }
    
    public static async Task<PaginationResponse<T>> ToPaginationResponseAsync<T>(this IQueryable<T> query, PaginationRequest request)
    {
        var total = await query.CountAsync();
        var data = await query.Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PaginationResponse<T>
        {
            Page = request.Page,
            PageSize = request.PageSize,
            Total = total,
            TotalPages = (int)Math.Ceiling(total / (double)request.PageSize),
            Data = data,
        };
    }
}