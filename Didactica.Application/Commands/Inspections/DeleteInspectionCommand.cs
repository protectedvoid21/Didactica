using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.Inspections;

public record DeleteInspectionCommand(
    int InspectionId
) : IRequest<Result>;
