using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.Inspections.Add;

public record AddInspectionCommand(
    int LessonId,
    int InspectionMethodId,
    int TeacherId,
    bool IsRemote,
    string? LessonEnvironment,
    int InspectionTypeId
) : IRequest<Result>;