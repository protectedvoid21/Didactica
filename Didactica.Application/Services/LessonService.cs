using Didactica.Domain.Dto;
using Didactica.Domain.Models;
using Didactica.Domain.Services;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Didactica.Application.Services;

public class LessonService : ILessonService
{
    private readonly IDbContext _dbContext;
    
    public LessonService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<IEnumerable<GetLessonResponse>>> GetAllAsync()
    {
        var lessons = await _dbContext.Lessons.Select(l => new GetLessonResponse
        {
            Name = l.Name,
            LessonType = l.LessonType.Id,
            Date = l.Date,
            Code = l.Code,
            Room = l.Room
        }).ToListAsync();
        return lessons;
    }

    public async Task<Result> AddAsync(CreateLessonRequest request)
    {
        var lessonType = await _dbContext.LessonTypes.FirstOrDefaultAsync(h => h.Id == request.LessonTypeId);
        if (lessonType == null)
        {
            return Result.Fail("Lesson type not found");
        }

        _dbContext.Lessons.Add(new Lesson
        {
            LessonType = lessonType,
            Code = request.Code,
            Date = request.Date,
            Name = request.Name,
            Room = request.Room
        });
        await _dbContext.SaveChangesAsync();
        return Result.Ok().WithSuccess("Lesson added successfully");
    }
}