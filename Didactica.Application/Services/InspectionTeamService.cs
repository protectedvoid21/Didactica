using System.Diagnostics;
using Didactica.Domain.Dto;
using Didactica.Domain.Models;
using Didactica.Domain.Services;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Didactica.Application.Services;

/// <summary>
/// Service responsible for managing inspection teams within the application.
/// </summary>
public class InspectionTeamService : IInspectionTeamService
{
    /// <summary>
    /// Represents the database context dependency injected into the service class, providing
    /// access to database entities and enabling CRUD operations on models such as InspectionTeams, Teachers, Inspections, etc.
    /// </summary>
    private readonly IDbContext _dbContext;

    /// Provides services related to managing inspection teams within the application.
    public InspectionTeamService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Adds a new inspection team to the database with the specified request details.
    /// </summary>
    /// <param name="request">
    /// Contains information about the inspection team, including the list of teacher IDs to be assigned.
    /// </param>
    /// <returns>
    /// Returns a <see cref="FluentResults.Result"/> indicating whether the operation was successful or failed.
    /// </returns>
    public async Task<Result> AddAsync(CreateInspectionTeamRequest request)
    {
        var inspectionTeam = new InspectionTeam(); 
        if(request.TeacherIds != null && request.TeacherIds.Count == 0)
        {
            return Result.Fail("At least one teacher must be assigned to the inspection team");
        }

        Debug.Assert(request.TeacherIds != null, "request.TeacherIds != null");
        foreach (var id in request.TeacherIds)
        {
            var teacher = await _dbContext.Teachers.FirstOrDefaultAsync(t => t.Id == id);
            if (teacher == null)
            {
                return Result.Fail($"Teacher with id {id} not found");
            }
            inspectionTeam.Teachers.Add(teacher);
        }
        _dbContext.InspectionTeams.Add(inspectionTeam);
        await _dbContext.SaveChangesAsync();
        return Result.Ok().WithSuccess("Inspection team added successfully");
    }
}