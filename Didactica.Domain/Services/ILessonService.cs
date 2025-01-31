using Didactica.Domain.Dto;
using FluentResults;

namespace Didactica.Domain.Services;

/// <summary>
/// Provides operations related to lesson management.
/// </summary>
public interface ILessonService
{
    Task<Result<IEnumerable<GetLessonResponse>>> GetAllAsync();

    Task<Result> AddAsync(CreateLessonRequest request);
}