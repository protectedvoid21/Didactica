using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.Inspections.Add;

public class AddInspectionHandler : IRequestHandler<AddInspectionCommand, Result>
{
    private readonly IInspectionService _inspectionService;

    public AddInspectionHandler(IInspectionService inspectionService)
    {
        _inspectionService = inspectionService;
    }

    public async Task<Result> Handle(AddInspectionCommand request, CancellationToken cancellationToken)
    {
        var addResult = await _inspectionService.AddAsync(new CreateInspectionRequest
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