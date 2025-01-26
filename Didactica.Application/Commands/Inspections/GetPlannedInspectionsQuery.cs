using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.Inspections;

public record GetPlannedInspectionsQuery : IRequest<Result<IEnumerable<GetInspectionResponse>>>;


public class GetPlannedInspectionsHandler(IInspectionService inspectionService)
    : IRequestHandler<GetPlannedInspectionsQuery, Result<IEnumerable<GetInspectionResponse>>>
{
    public async Task<Result<IEnumerable<GetInspectionResponse>>> Handle(GetPlannedInspectionsQuery request, CancellationToken cancellationToken)
    {
        return await inspectionService.GetAllPLanedInspections();
    }
}