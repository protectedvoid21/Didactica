using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.InspectionTeam;

public class GetInspectionsForTeamTeacherHandler : IRequestHandler<GetInspectionsForTeamTeacherQuery, Result<IEnumerable<GetInspectionResponse>>>
{
    private readonly IInspectionService _inspectionService;

    public GetInspectionsForTeamTeacherHandler(IInspectionService inspectionService)
    {
        _inspectionService = inspectionService;
    }

    public async Task<Result<IEnumerable<GetInspectionResponse>>> Handle(GetInspectionsForTeamTeacherQuery request, CancellationToken ct)
    {
        return await _inspectionService.GetInspectionsForTeamTeacher(request.TeacherId);
    }
}