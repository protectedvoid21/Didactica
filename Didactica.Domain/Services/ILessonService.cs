using Didactica.Domain.Dto;
using FluentResults;

namespace Didactica.Domain.Services;

public interface ILessonService
{
    Task<Result<IEnumerable<GetLessonResponse>>> GetAllAsync();

    Task<Result> AddAsync(CreateLessonRequest request);
}