using System.Linq.Expressions;
using Didactica.Domain.Dto;
using Didactica.Domain.Models;
using Didactica.Domain.Models.Persistent;
using Didactica.Domain.Services;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Didactica.Application.Services;

/// <summary>
/// The InspectionService class provides functionality for managing inspections within the application.
/// Implements the <see cref="IInspectionService"/> interface and interacts with the database context
/// to perform operations related to inspections, such as retrieving, adding, and deleting inspections.
/// </summary>
public class InspectionService : IInspectionService
{
    /// Represents the database context used within the service for accessing and interacting with the database.
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Provides operations related to managing inspections, including retrieving, adding,
    /// and deleting inspection records, handling inspection data for teachers, and working with planned inspections.
    /// Implements the <see cref="IInspectionService"/> interface.
    /// </summary>
    public InspectionService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Retrieves an inspection record by its unique identifier and returns a detailed response with its associated data.
    /// </summary>
    /// <param name="id">The unique identifier of the inspection to be retrieved.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the inspection details wrapped in a <see cref="GetInspectionResponse"/> object
    /// if the inspection is found, or a failure result if not.
    /// </returns>
    public async Task<Result<GetInspectionResponse>> GetAsync(int id)
    {
        var inspection = await _dbContext.Inspections.Select(InspectionExpression)
            .FirstOrDefaultAsync(h => h.Id == id);
        
        if (inspection == null)
        {
            return Result.Fail("Inspection not found");
        }
        return inspection;
    }

    /// <summary>
    /// Retrieves a list of inspections associated with a specific teacher asynchronously.
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher whose inspections should be retrieved.</param>
    /// <param name="ct">The cancellation token used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a Result object that either contains a list of GetInspectionResponse objects or an error message if the teacher is not found.</returns>
    public async Task<Result<List<GetInspectionResponse>>> GetInspectionsForTeacherAsync(int teacherId, CancellationToken ct)
    {
        var teacher = await _dbContext.Teachers.FirstOrDefaultAsync(h => h.Id == teacherId, ct);
        if (teacher == null)
        {
            return Result.Fail("Teacher not found");
        }
        
        var inspections = await _dbContext.Inspections
            .Where(h => h.Teacher.Id == teacherId)
            .Select(InspectionExpression)
            .ToListAsync(ct);
        
        return Result.Ok(inspections);
    }

    /// Asynchronously adds a new inspection to the database.
    /// Validates the provided teacher and lesson IDs before creating the inspection.
    /// If the teacher or lesson is not found, the operation fails.
    /// <param name="request">The request object containing details required to create the inspection, such as LessonId, TeacherId, IsRemote, and LessonEnvironment.</param>
    /// <return>A Result object indicating whether the operation was successful or failed, along with any associated messages.</return>
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
            LessonEnvironment = request.LessonEnvironment
        });
        await _dbContext.SaveChangesAsync();
        return Result.Ok().WithSuccess("Inspection added successfully");
    }

    /// Deletes an inspection from the database.
    /// <param name="request">The request object containing the ID of the inspection to be deleted.</param>
    /// <returns>A result indicating success or failure of the deletion operation, with an optional success message or error details.</returns>
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

    /// <summary>
    /// Retrieves all inspections associated with a specific teacher by their unique identifier.
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher whose inspections are to be retrieved.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing a result that includes
    /// an enumerable collection of <see cref="GetInspectionResponse"/> objects if successful.
    /// </returns>
    public async Task<Result<IEnumerable<GetInspectionResponse>>> GetInspectionsOfTeacherById(int teacherId)
    {
        var inspections = await _dbContext.Inspections.Select(InspectionExpression)
            .Where(h => h.TeacherId == teacherId)
            .ToListAsync();
        
        return inspections;
    }

    /// <summary>
    /// Retrieves all planned inspections that are scheduled to take place in the future.
    /// </summary>
    /// <returns>
    /// A task result containing a collection of <see cref="GetInspectionResponse"/> objects representing the
    /// planned inspections. Each object includes details such as teacher information, course details,
    /// inspection time, location, and associated inspection team information.
    /// </returns>
    public async Task<Result<IEnumerable<GetInspectionResponse>>> GetAllPlannedInspections()
    {
        var inspections = await _dbContext.Inspections
            .Where(h => h.Lesson.Date > DateTime.UtcNow)
            .Select(InspectionExpression)
            .ToListAsync();
        
        return inspections;
    }

    public async Task<Result<IEnumerable<GetInspectionResponse>>> GetInspectionsForTeamTeacher(int teacherId)
    {
        var teacher = await _dbContext.Teachers.FirstOrDefaultAsync(h => h.Id == teacherId);
        if (teacher == null)
        {
            return Result.Fail("Teacher not found");
        }
        
        var inspections = await _dbContext.Inspections
            .Where(i => i.InspectionTeam != null && i.InspectionTeam.Teachers.Any(t => t.Id == teacherId) == true)
            .Select(InspectionExpression)
            .ToListAsync();
        
        return inspections;
    }

    /// <summary>
    /// Adds a new inspection form to the database based on the provided request details.
    /// </summary>
    /// <param name="inspectionId">Identifier of the inspection to which the form is related.</param>
    /// <param name="request">The details of the inspection form to be added, including attributes such as attendance, room suitability, final grade, and related inspection ID.</param>
    /// <returns>A Result object indicating success or failure. On success, the result contains a success message. On failure, it contains the error details.</returns>
    public async Task<Result> AddFormAsync(int inspectionId, AddInspectionFormRequest request)
    {
        var inspection = await _dbContext.Inspections.FirstOrDefaultAsync(h => h.Id == inspectionId);
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
    
    private static Expression<Func<Inspection, GetInspectionResponse>> InspectionExpression = inspection =>
        new GetInspectionResponse
        {
            Id = inspection.Id,
            TeacherId = inspection.Teacher.Id,
            TeacherFirstName = inspection.Teacher.Name,
            TeacherLastName = inspection.Teacher.LastName,
            Course = inspection.Lesson.Name,
            CourseType = inspection.Lesson.LessonType.Name,
            Date = inspection.Lesson.Date,
            IsRemote = inspection.IsRemote,
            LessonEnvironment = inspection.LessonEnvironment,
            Place = inspection.Lesson.Room,
            GetInspectionTeamResponse = new GetInspectionTeamBasicResponse
            {
                Id = inspection.InspectionTeam!.Id,
                Teachers = inspection.InspectionTeam.Teachers.Select(t => new Tuple<int, string>(
                        t.Id,
                        string.Join(" ",
                            t.Degree != null
                                ? t.Degree.Short
                                : "",
                            t.Name,
                            t.LastName)))
                    .ToArray(),
            },
            IsRated = inspection.InspectionForm != null,
            InspectionFormId = inspection.InspectionForm != null ? inspection.InspectionForm.Id : null,
        };
}