using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.InspectionTeam;

public record AddInspectionTeamCommand
(
    List<int> TeacherIds
) : IRequest<Result>;