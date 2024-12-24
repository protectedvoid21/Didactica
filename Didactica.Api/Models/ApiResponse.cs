namespace Didactica.Api.Models;

public class ApiResponse
{
    public List<string> Message { get; set; } = [];
    public bool IsSuccess { get; set; }
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }
}