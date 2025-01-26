using System.Collections;
using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.Lessons;

public record GetLessonsQuery() : IRequest<Result<IEnumerable<GetLessonResponse>>>;

public class GetLessonsHandler(ILessonService lessonService):
    IRequestHandler<GetLessonsQuery, Result<IEnumerable<GetLessonResponse>>>
{
    public async Task<Result<IEnumerable<GetLessonResponse>>> Handle(GetLessonsQuery request, CancellationToken cancellationToken)
    {
        return await lessonService.GetAllAsync();
    }
}