using Didactica.Application.Services;
using Didactica.Domain.Dto;
using Didactica.Domain.Models;
using Didactica.Domain.Models.Persistent;
using Didactica.Domain.Services;
using MockQueryable.NSubstitute;
using NSubstitute;
using Shouldly;

namespace Didactica.Tests;

public class InspectionTeamServiceTests
{
    private readonly IDbContext _dbContext = Substitute.For<IDbContext>();
    private readonly InspectionTeamService _inspectionTeamService;
    
    public InspectionTeamServiceTests()
    {
        _inspectionTeamService = new InspectionTeamService(_dbContext);
    }
    
    [Fact]
    public async Task Add_NewInspectionTeamWithNotExistingTeacher_ReturnsFail()
    {
        // Arrange
        var teacherList = new List<Teacher>();
        var mock = teacherList.AsQueryable().BuildMockDbSet();
        _dbContext.Teachers.Returns(mock);
        
        // Act
        var result = await _inspectionTeamService.AddAsync(new CreateInspectionTeamRequest
        {
            TeacherIds = [1]
        });
        
        // Assert
        result.IsSuccess.ShouldBeFalse();
    }
    
    [Fact]
    public async Task Add_NewInspectionTeamWithExistingTeacher_ReturnsSuccess()
    {
        // Arrange
        var teacherList = new List<Teacher>{
            new()
            {
                Id = 1,
                Name = "John",
                LastName = "Doe",
                Email = "jdoe@uni.com",
                PhoneNumber = "123456789",
                Faculty = "Math"
            }
        };
        var mock = teacherList.AsQueryable().BuildMockDbSet();
        _dbContext.Teachers.Returns(mock);
        
        // Act
        var result = await _inspectionTeamService.AddAsync(new CreateInspectionTeamRequest
        {
            TeacherIds = [1]
        });
        
        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
    
    [Fact]
    public async Task Add_NewInspectionTeamWithExistingTeacherAndNonExistingAtOnce_ReturnsFail()
    {
        // Arrange
        var teacherList = new List<Teacher>{
            new()
            {
                Id = 1,
                Name = "John",
                LastName = "Doe",
                Email = "jdoe@uni.com",
                PhoneNumber = "123456789",
                Faculty = "Math"
            },
        };
        var mock = teacherList.AsQueryable().BuildMockDbSet();
        _dbContext.Teachers.Returns(mock);
        
        // Act
        var result = await _inspectionTeamService.AddAsync(new CreateInspectionTeamRequest
        {
            TeacherIds = [1, 2]
        });
        
        // Assert
        result.IsSuccess.ShouldBeFalse();
    }
    
    [Fact]
    public async Task Add_NewInspectionTeamWithMultiplePresentTeachers_ReturnsSuccess()
    {
        // Arrange
        var teacherList = new List<Teacher>{
            new()
            {
                Id = 1,
                Name = "John",
                LastName = "Doe",
                Email = "jdoe@uni.com",
                PhoneNumber = "123456789",
                Faculty = "Math"
            },
            new()
            {
                Id = 2,
                Name = "Jane",
                LastName = "Doe",
                Email = "janedoe@uni.com",
                PhoneNumber = "987654321",
                Faculty = "Physics"
            },
            new()
            {
                Id = 3,
                Name = "Alice",
                LastName = "Smith",
                Email = "asmith@uni.com",
                PhoneNumber = "456123789",
                Faculty = "Chemistry"
            }
        };
        var mock = teacherList.AsQueryable().BuildMockDbSet();
        _dbContext.Teachers.Returns(mock);
        
        // Act
        var result = await _inspectionTeamService.AddAsync(new CreateInspectionTeamRequest
        {
            TeacherIds = [1, 2, 3]
        });
        
        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
    [Fact]
    public async Task Add_NewInspectionTeamWithNoTeachersProvided_ReturnsFalse()
    {
        // Arrange
        var teacherList = new List<Teacher>();
        var mock = teacherList.AsQueryable().BuildMockDbSet();
        _dbContext.Teachers.Returns(mock);
        
        // Act
        var result = await _inspectionTeamService.AddAsync(new CreateInspectionTeamRequest
        {
            TeacherIds = []
        });
        
        // Assert
        result.IsSuccess.ShouldBeFalse();
    }
}