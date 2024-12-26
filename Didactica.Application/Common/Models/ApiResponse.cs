namespace Didactica.Application.Common.Models;

public class ApiResponse
{
    public List<string> Message { get; set; } = [];
    public bool IsSuccess { get; set; }
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }
}