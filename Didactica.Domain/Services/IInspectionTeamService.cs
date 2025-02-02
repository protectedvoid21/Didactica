using Didactica.Domain.Dto;
using FluentResults;

namespace Didactica.Domain.Services;

/// <summary>
/// Defines the contract for services related to managing inspection teams.
/// </summary>
public interface IInspectionTeamService
{
    Task<Result> AddAsync(CreateInspectionTeamRequest request);
    
    Task<Result<IEnumerable<GetInspectionTeamResponse>>> GetAllAsync();
}