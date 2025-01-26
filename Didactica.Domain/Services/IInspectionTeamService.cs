using Didactica.Domain.Dto;
using FluentResults;

namespace Didactica.Domain.Services;

public interface IInspectionTeamService
{
    Task<Result> AddAsync(CreateInspectionTeamRequest request);
}