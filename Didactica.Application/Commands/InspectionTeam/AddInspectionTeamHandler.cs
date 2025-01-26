using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.InspectionTeam;

public class AddInspectionTeamHandler(IInspectionTeamService inspectionTeamService): IRequestHandler<AddInspectionTeamCommand, Result>
{
    public async Task<Result> Handle(AddInspectionTeamCommand request, CancellationToken cancellationToken)
    {
        var addResult = await inspectionTeamService.AddAsync(new CreateInspectionTeamRequest
        {
           TeacherIds = request.TeacherIds,
        });
        
        return addResult;
    }
}