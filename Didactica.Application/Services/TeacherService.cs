using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Didactica.Application.Services;

/// <summary>
/// Provides services for managing teacher data.
/// </summary>
public class TeacherService : ITeacherService
{
    /// <summary>
    /// Represents the database context used to access and manage data within the application.
    /// </summary>
    /// <remarks>
    /// This provides an abstraction over Entity Framework Core's DbContext, allowing
    /// for querying and saving data related to entities such as Teachers, Inspections,
    /// Lessons, and more. It is used for performing database operations asynchronously
    /// within the service layers of the application.
    /// </remarks>
    private readonly IDbContext _dbContext;

    /// Represents the service for managing teacher-related operations.
    public TeacherService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Retrieves all teacher records from the data source and transforms them into a collection of <see cref="GetTeacherResponse"/> objects.
    /// </summary>
    /// <returns>
    /// A result containing a collection of <see cref="GetTeacherResponse"/> objects if successful,
    /// or an error result if the operation fails.
    /// </returns>
    public async Task<Result<IEnumerable<GetTeacherResponse>>> GetAllAsync()
    {
        var teachers = await _dbContext.Teachers.Select(t => new GetTeacherResponse
        {
            Id = t.Id,
            Name = t.Name,
            LastName = t.LastName,
            Faculty = t.Faculty,
            Email = t.Email,
            PhoneNumber = t.PhoneNumber
        }).ToListAsync();
        
        return teachers;
    }

    /// <summary>
    /// Retrieves a teacher by their unique identifier asynchronously.
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher to retrieve.</param>
    /// <returns>
    /// A task representing the asynchronous operation. The task result contains a
    /// <see cref="FluentResults.Result{T}"/> where T is <see cref="GetTeacherResponse"/>.
    /// Returns a successful result containing the teacher's information if found,
    /// otherwise returns a failure result.
    /// </returns>
    public async Task<Result<GetTeacherResponse>> GetByIdAsync(int teacherId)
    {
        var teacher = await _dbContext.Teachers.Select(t => new GetTeacherResponse
        {
            Id = t.Id,
            Name = t.Name,
            LastName = t.LastName,
            Faculty = t.Faculty,
            Email = t.Email,
            PhoneNumber = t.PhoneNumber
        }).FirstOrDefaultAsync(t => t.Id == teacherId);

        return teacher ?? (Result<GetTeacherResponse>)Result.Fail("Inspection not found");
    }
}