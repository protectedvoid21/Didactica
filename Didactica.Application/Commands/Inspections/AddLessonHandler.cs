using Didactica.Application.Services;
using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.Inspections;

public class AddLessonHandler(ILessonService lessonService): IRequestHandler<AddLessonCommand, Result>
{
    public async Task<Result> Handle(AddLessonCommand request, CancellationToken cancellationToken)
    {
        var addResult = await lessonService.AddAsync(new CreateLessonRequest
        {
            LessonTypeId = request.LessonTypeId,
            Code = request.Code,
            Name = request.Name, 
            Room = request.Room
        });
        
        return addResult;
    }
    
}