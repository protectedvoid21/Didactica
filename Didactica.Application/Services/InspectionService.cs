using Didactica.Domain.Dto;
using Didactica.Domain.Models;
using Didactica.Domain.Services;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Didactica.Application.Services;

public class InspectionService : IInspectionService
{
    private readonly IDbContext _dbContext;
    
    public InspectionService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetInspectionResponse>> GetAsync(int id)
    {
        var inspection = await _dbContext.Inspections.Select(h => new GetInspectionResponse
        {
            Id = h.Id,
            TeacherId = h.Teacher.Id,
            TeacherFirstName = h.Teacher.Name,
            TeacherLastName = h.Teacher.LastName,
            Course = h.Lesson.Name,
            CourseType = h.Lesson.LessonType.Name,
            Date = h.Lesson.Date,
            IsRemote = h.IsRemote,
            LessonEnvironment = h.LessonEnvironment,
            Place = h.Lesson.Room,
            GetInspectionTeamResponse = new GetInspectionTeamResponse
            {
                Id = h.InspectionTeam.Id,
                Teachers = h.InspectionTeam.Teachers.Select(t => new Tuple<int, string>(t.Id, string.Join(" ", t.Name, t.LastName))).ToArray(),
            }
        }).FirstOrDefaultAsync(h => h.Id == id);
        
        if (inspection == null)
        {
            return Result.Fail("Inspection not found");
        }
        return inspection;
    }

    public async Task<Result<List<GetInspectionResponse>>> GetInspectionsForTeacherAsync(int teacherId, CancellationToken ct)
    {
        var teacher = await _dbContext.Teachers.FirstOrDefaultAsync(h => h.Id == teacherId, ct);
        if (teacher == null)
        {
            return Result.Fail("Teacher not found");
        }
        
        var inspections = await _dbContext.Inspections
            .Where(h => h.Teacher.Id == teacherId)
            .Select(h => new GetInspectionResponse
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
                TeacherId = 0,
                GetInspectionTeamResponse = null,
            }).ToListAsync(ct);
        
        return Result.Ok(inspections);
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
        
        _dbContext.Inspections.Add(new Inspection
        {
            Teacher = teacher,
            Lesson = lesson,
            IsRemote = request.IsRemote,
            LessonEnvironment = request.LessonEnvironment,
        });
        await _dbContext.SaveChangesAsync();
        return Result.Ok().WithSuccess("Inspection added successfully");
    }
    
    public async Task<Result> DeleteAsync(DeleteInspectionRequest request)
    {
        var inspection = await _dbContext.Inspections.FirstOrDefaultAsync(h => h.Id == request.InspectionId);
        if (inspection == null)
        {
            return Result.Fail("Inspection not found");
        }
        
        _dbContext.Inspections.Remove(inspection);
        await _dbContext.SaveChangesAsync();
        return Result.Ok().WithSuccess("Inspection deleted successfully");
    }

    public async Task<Result<IEnumerable<GetInspectionResponse>>> GetInspectionsOfTeacherById(int teacherId)
    {
        var inspections = await _dbContext.Inspections.Select(h => new GetInspectionResponse
        {
            Id = h.Id,
            TeacherId = h.Teacher.Id,
            TeacherFirstName = h.Teacher.Name,
            TeacherLastName = h.Teacher.LastName,
            Course = h.Lesson.Name, 
            CourseType = h.Lesson.LessonType.Name,
            Date = h.Lesson.Date,
            IsRemote = h.IsRemote,
            LessonEnvironment = h.LessonEnvironment,
            Place = h.Lesson.Room,
            GetInspectionTeamResponse = new GetInspectionTeamResponse
            {
                Id = h.InspectionTeam.Id,
                Teachers = h.InspectionTeam.Teachers.Select(t => new Tuple<int, string>(t.Id, string.Join(" ", t.Name, t.LastName))).ToArray(),
            }
        }).Where(h => h.Id == teacherId).ToListAsync();
        return inspections;
    }

    public async Task<Result<IEnumerable<GetInspectionResponse>>> GetAllPLanedInspections()
    {
        var inspections = await _dbContext.Inspections.Select(h => new GetInspectionResponse
        {
            Id = h.Id,
            TeacherId = h.Teacher.Id,
            TeacherFirstName = h.Teacher.Name,
            TeacherLastName = h.Teacher.LastName,
            Course = h.Lesson.Name,
            CourseType = h.Lesson.LessonType.Name,
            Date = h.Lesson.Date,
            IsRemote = h.IsRemote,
            LessonEnvironment = h.LessonEnvironment,
            Place = h.Lesson.Room,
            GetInspectionTeamResponse = new GetInspectionTeamResponse
            {
                Id = h.InspectionTeam.Id,
                Teachers = h.InspectionTeam.Teachers.Select(t => new Tuple<int, string>(t.Id, string.Join(" ", t.Name, t.LastName))).ToArray(),
            }
        }).Where(h => h.Date > DateTime.UtcNow).ToListAsync();
        
        return inspections;
    }
    
    public async Task<Result> AddFormAsync(AddInspectionFormRequest request)
    {
        var inspection = await _dbContext.Inspections.FirstOrDefaultAsync(h => h.Id == request.InspectionId);
        if (inspection == null)
        {
            return Result.Fail("Inspection not found");
        }
        
        _dbContext.InspectionForms.Add(new InspectionForm
        {
            InspectionId = inspection.Id,
            WasAttendanceChecked = request.WasAttendanceChecked,
            WereClassesOnTime = request.WereClassesOnTime,
            WasRoomSuitable = request.WasRoomSuitable,
            PresentedTopicAndScope = request.PresentedTopicAndScope,
            ExplainedClearly = request.ExplainedClearly,
            WasEngaged = request.WasEngaged,
            EncouragedIndependentThinking = request.EncouragedIndependentThinking,
            MaintainedDocumentation = request.MaintainedDocumentation,
            DeliveredUpdatedKnowledge = request.DeliveredUpdatedKnowledge,
            PresentedPreparedMaterial = request.PresentedPreparedMaterial,
            FinalGradeJustification = request.FinalGradeJustification,
            ConclusionsAndRecommendations = request.ConclusionsAndRecommendations,
            FinalGrade = request.FinalGrade,
        });
        await _dbContext.SaveChangesAsync();
        return Result.Ok().WithSuccess("Inspection form added successfully");
    }
}