using Didactica.Domain.Dto;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.InspectionTeam;

public class GetInspectionsTeamsQuery : IRequest<Result<IEnumerable<GetInspectionTeamResponse>>>;