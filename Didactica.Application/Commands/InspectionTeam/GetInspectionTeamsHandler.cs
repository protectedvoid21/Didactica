using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.InspectionTeam;

public class GetInspectionTeamsHandler : IRequestHandler<GetInspectionsTeamsQuery, Result<IEnumerable<GetInspectionTeamResponse>>>
{
    private readonly IInspectionTeamService _inspectionTeamService;
    
    public GetInspectionTeamsHandler(IInspectionTeamService inspectionTeamService)
    {
        _inspectionTeamService = inspectionTeamService;
    }
    
    public async Task<Result<IEnumerable<GetInspectionTeamResponse>>> Handle(GetInspectionsTeamsQuery request, CancellationToken cancellationToken)
    {
        return await _inspectionTeamService.GetAllAsync();
    }
}