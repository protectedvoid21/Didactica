using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.Teachers;

public record GetTeacherQuery(int TeacherId) : IRequest<Result<GetTeacherResponse>>;

public class GetTeacherHandler(ITeacherService teacherService)
    : IRequestHandler<GetTeacherQuery, Result<GetTeacherResponse>>
{
    public async Task<Result<GetTeacherResponse>> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
    {
        return await teacherService.GetByIdAsync(request.TeacherId);
    }
}