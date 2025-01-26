using Didactica.Domain.Dto;
using FluentResults;

namespace Didactica.Domain.Services;

public interface ITeacherService
{
    Task<Result<IEnumerable<GetTeacherResponse>>> GetAllAsync();
    Task<Result<GetTeacherResponse>> GetByIdAsync(int teacherId);

}