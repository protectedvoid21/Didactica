using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.Inspections;

public record AddLessonCommand(
        string Code,
        string Name,
        DateTime Date,
        string? Room,
        int LessonTypeId
) : IRequest<Result>;