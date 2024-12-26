namespace Didactica.Application.Common.Models;

public record PaginationRequest
{
    public int Page { get; init; }
    public int PageSize { get; init; }
}