using Didactica.Api.Models;
using FluentResults;

namespace Didactica.Api.Helpers;

public static class ResultsExtensions
{
    public static ApiResponse ToApiResponse(this Result result)
    {
        return new ApiResponse
        {
            IsSuccess = result.IsSuccess,
            Message = result.JoinMessages(),
        };
    }
    
    public static ApiResponse<T> ToApiResponse<T>(this Result<T> result)
    {
        return new ApiResponse<T>
        {
            IsSuccess = result.IsSuccess,
            Message = result.JoinMessages(),
            Data = result.Value,
        };
    }
    
    public static List<string> JoinMessages(this Result result)
    {
        return result.Errors.Select(e => e.Message).ToList();
    }

    public static List<string> JoinMessages<T>(this Result<T> result)
    {
        return result.Errors.Select(e => e.Message).ToList();
    }
}