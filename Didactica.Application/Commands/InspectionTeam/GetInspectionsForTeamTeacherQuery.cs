using Didactica.Domain.Dto;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.InspectionTeam;

public record GetInspectionsForTeamTeacherQuery(int TeacherId) : IRequest<Result<IEnumerable<GetInspectionResponse>>>;