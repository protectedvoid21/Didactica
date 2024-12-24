using Didactica.Api.Dto;
using Didactica.Api.Models;
using Didactica.Api.Persistence;
using Didactica.Api.Persistence.Entities;
using Didactica.Api.Service.Interfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Didactica.Api.Service;

public class InspectionService : IInspectionService
{
    private readonly DidacticaDbContext _dbContext;
    
    public InspectionService(DidacticaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetInspectionResponse?>> GetAsync(int id)
    {
        var inspection = await _dbContext.Inspections.Select(h => new GetInspectionResponse
        {
            Id = h.Id,
            TeacherFirstName = h.Teacher.Name,
            TeacherLastName = h.Teacher.LastName,
            Course = h.Lesson.Name,
            CourseType = h.Lesson.LessonType.Name,
            Date = h.Lesson.Date,
            IsRemote = h.IsRemote,
            LessonEnvironment = h.LessonEnvironment,
            Place = h.Lesson.Room,
        }).FirstOrDefaultAsync(h => h.Id == id);
        
        if (inspection == null)
        {
            return Result.Fail("Inspection not found");
        }
        return inspection;
    }

    public async Task<Result> AddAsync(CreateInspectionRequest request)
    {
        var teacher = await _dbContext.Teachers.FirstOrDefaultAsync(h => h.Id == request.TeacherId);
        if (teacher == null)
        {
            return Result.Fail("Teacher not found");
        }
        
        var lesson = await _dbContext.Lessons.FirstOrDefaultAsync(h => h.Id == request.LessonId);
        if (lesson == null)
        {
            return Result.Fail("Lesson not found");
        }
        
        var inspectionMethod = await _dbContext.InspectionMethods.FirstOrDefaultAsync(h => h.Id == request.InspectionMethodId);
        if (inspectionMethod == null)
        {
            return Result.Fail("Inspection method not found");
        }
        
        _dbContext.Add(new Inspection
        {
            Teacher = teacher,
            Lesson = lesson,
            IsRemote = request.IsRemote,
            LessonEnvironment = request.LessonEnvironment,
            InspectionMethod = inspectionMethod,
        });
        await _dbContext.SaveChangesAsync();
        return Result.Ok().WithSuccess("Inspection added successfully");
    }
}