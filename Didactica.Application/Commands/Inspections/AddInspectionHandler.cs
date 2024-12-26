using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.Inspections;

public class AddInspectionHandler(IInspectionService inspectionService) : IRequestHandler<AddInspectionCommand, Result>
{
    public async Task<Result> Handle(AddInspectionCommand request, CancellationToken cancellationToken)
    {
        var addResult = await inspectionService.AddAsync(new CreateInspectionRequest
        {
            LessonId = request.LessonId,
            InspectionMethodId = request.InspectionMethodId,
            TeacherId = request.TeacherId,
            IsRemote = request.IsRemote,
            LessonEnvironment = request.LessonEnvironment,
        });
        
        return addResult;
    }
}