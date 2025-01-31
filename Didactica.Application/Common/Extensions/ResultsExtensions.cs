using Didactica.Application.Common.Models;
using FluentResults;

namespace Didactica.Application.Common.Extensions;

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
            Data = result.Value
        };
    }

    private static List<string> JoinMessages(this Result result)
    {
        return result.Errors.ConvertAll(e => e.Message);
    }

    private static List<string> JoinMessages<T>(this Result<T> result)
    {
        return result.Errors.ConvertAll(e => e.Message);
    }
}