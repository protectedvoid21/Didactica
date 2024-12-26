using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.Inspections;

public record GetInspectionQuery(int Id) : IRequest<Result<GetInspectionResponse>>;

public class GetInspectionHandler(IInspectionService inspectionService)
    : IRequestHandler<GetInspectionQuery, Result<GetInspectionResponse>>
{
    public async Task<Result<GetInspectionResponse>> Handle(GetInspectionQuery request, CancellationToken cancellationToken)
    {
        return await inspectionService.GetAsync(request.Id);
    }
}