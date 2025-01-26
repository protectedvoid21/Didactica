using System.Collections;
using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Didactica.Application.Services;

public class TeacherService : ITeacherService
{
    private readonly IDbContext _dbContext;
    
    public TeacherService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

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
        
        if (teacher == null)
        {
            return Result.Fail("Inspection not found");
        }
        
        return teacher;
    }
}