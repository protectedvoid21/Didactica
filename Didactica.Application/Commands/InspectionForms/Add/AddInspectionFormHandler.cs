using Didactica.Application.Commands.InspectionForms.Submit;
using Didactica.Domain.Services;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.InspectionForms.Add;

public class AddInspectionFormHandler(IInspectionService inspectionService) 
    : IRequestHandler<AddInspectionFormCommand, Result>
{
    public async Task<Result> Handle(AddInspectionFormCommand request, CancellationToken cancellationToken)
    {
        return await inspectionService.AddFormAsync(request.Request);
    }
}