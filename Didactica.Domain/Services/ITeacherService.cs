using Didactica.Domain.Dto;
using FluentResults;

namespace Didactica.Domain.Services;

public interface ITeachersService
{
    Task<Result<GetTeacherResponse>> GetAsync();
}