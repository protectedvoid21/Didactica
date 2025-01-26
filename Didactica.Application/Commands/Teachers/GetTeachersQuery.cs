using Didactica.Application.Commands.Inspections;
using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using FluentResults;
using MediatR;
using Didactica.Domain.Dto;
using Didactica.Domain.Services;

namespace Didactica.Application.Commands.Teachers;

public record GetTeachersQuery() : IRequest<Result<IEnumerable<GetTeacherResponse>>>;

public class GetTeachersHandler(ITeacherService teachersService)
    : IRequestHandler<GetTeachersQuery, Result<IEnumerable<GetTeacherResponse>>>
{
    public async Task<Result<IEnumerable<GetTeacherResponse>>> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
    {
        return await teachersService.GetAllAsync();
    }
}