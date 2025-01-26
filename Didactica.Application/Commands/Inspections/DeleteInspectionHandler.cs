using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.Inspections;

public class DeleteInspectionHandler(IInspectionService inspectionService) : IRequestHandler<DeleteInspectionCommand, Result>
{
    public async Task<Result> Handle(DeleteInspectionCommand request, CancellationToken cancellationToken)
    {
        var deleteResult = await inspectionService.DeleteAsync(new DeleteInspectionRequest
        {
            InspectionId = request.InspectionId
        });
        return deleteResult;
    }
}