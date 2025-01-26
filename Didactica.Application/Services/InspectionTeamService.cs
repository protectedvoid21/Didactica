using Didactica.Domain.Dto;
using Didactica.Domain.Models;
using Didactica.Domain.Services;
using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Didactica.Application.Services;

public class InspectionTeamService: IInspectionTeamService
{
    private readonly IDbContext _dbContext;
    
    public InspectionTeamService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> AddAsync(CreateInspectionTeamRequest request)
    {
        var teachers = new List<Teacher>();

        var inspectionTeam = new InspectionTeam(); 
        foreach (var id in request.TeacherIds)
        {
            var teacher = _dbContext.Teachers.FirstOrDefault(t => t.Id == id);
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