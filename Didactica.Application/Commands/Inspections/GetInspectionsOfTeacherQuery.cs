using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.Inspections;

public record GetInspectionsOfTeacher(int Id) : IRequest<Result<IEnumerable<GetInspectionResponse>>>;

public class GetInspectionsOfTeacherHandler(IInspectionService inspectionService)
    : IRequestHandler<GetInspectionQuery, Result<GetInspectionResponse>>
{
    public async Task<Result<GetInspectionResponse>> Handle(GetInspectionsOfTeacher request, CancellationToken cancellationToken)
    {
        return await inspectionService.GetAsync(request.Id);
    }
}