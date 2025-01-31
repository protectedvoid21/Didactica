using Didactica.Domain.Dto;
using FluentResults;

namespace Didactica.Domain.Services;

/// <summary>
/// Provides functionality to manage and retrieve teacher information.
/// </summary>
public interface ITeacherService
{
    Task<Result<IEnumerable<GetTeacherResponse>>> GetAllAsync();
    Task<Result<GetTeacherResponse>> GetByIdAsync(int teacherId);
}