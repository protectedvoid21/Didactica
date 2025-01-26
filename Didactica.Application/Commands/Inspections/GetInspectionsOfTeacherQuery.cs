using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.Inspections;

public record GetInspectionsOfTeacherQuery(int Id) : IRequest<Result<IEnumerable<GetInspectionResponse>>>;

public class GetInspectionsOfTeacherHandler(IInspectionService inspectionService)
    : IRequestHandler<GetInspectionsOfTeacherQuery, Result<IEnumerable<GetInspectionResponse>>>
{
    public async Task<Result<IEnumerable<GetInspectionResponse>>> Handle(GetInspectionsOfTeacherQuery request, CancellationToken cancellationToken)
    {
        return await inspectionService.GetInspectionsOfTeacherById(request.Id);
    }
}