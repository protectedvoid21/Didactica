namespace Didactica.Application.Common.Models;

public record PaginationResponse<T>
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int Total { get; init; }
    public int TotalPages { get; init; }
    public List<T> Data { get; init; } = [];
}