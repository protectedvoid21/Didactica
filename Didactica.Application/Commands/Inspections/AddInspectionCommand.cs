using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.Inspections;

public record AddInspectionCommand(
    int LessonId,
    int InspectionMethodId,
    int TeacherId,
    bool IsRemote,
    string? LessonEnvironment,
    int InspectionTypeId
) : IRequest<Result>;