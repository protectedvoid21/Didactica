using Didactica.Application.Services;
using Didactica.Domain.Dto;
using Didactica.Domain.Models;
using Didactica.Domain.Services;
using MockQueryable.NSubstitute;
using NSubstitute;
using Shouldly;

namespace Didactica.Tests;

public class LessonServiceTests
{
    private readonly IDbContext _dbContext = Substitute.For<IDbContext>();
    private readonly LessonService _lessonService;
    public LessonServiceTests()
    {
        _lessonService = new LessonService(_dbContext);
    }
    
    [Fact]
    public async Task Get_AllLessonsEmptySet_ReturnsSeccess()
    {
        // Arrange
        var lessonList = new List<Lesson>();
        var mock = lessonList.AsQueryable().BuildMockDbSet();
        _dbContext.Lessons.Returns(mock);

        // Act
        var result = await _lessonService.GetAllAsync();

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
    [Fact]
    public async Task Get_AllLessonsSetNotEmpty_ReturnsSuccess()
    {
        // Arrange
        var lessonsList = new List<Lesson>
        {
            new()
            {
                Code = "123",
                Name = "Math",
                Room = "A1",
                Date = DateTime.Now,
                LessonType = new LessonType
                {
                    Id = 1,
                    Name = "Lecture"
                }
            }
        };
        var mock = lessonsList.AsQueryable().BuildMockDbSet();
        _dbContext.Lessons.Returns(mock);

        // Act
        var result = await _lessonService.GetAllAsync();

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
    [Fact]
     public async Task Add_LessonWrongLessonType_ReturnsFailure()
     {
         // Arrange
         var lessonList = new List<LessonType>
         {
             new()
             {
                 Id = 1,
                 Name = "Lecture"
             }
         };
         var mock = lessonList.AsQueryable().BuildMockDbSet();
         _dbContext.LessonTypes.Returns(mock);
    
         // Act
         var result = await _lessonService.AddAsync(new CreateLessonRequest
         {
             Code = "123",
             Name = "Math",
             Room = "A1",
             Date = DateTime.Now,
             LessonTypeId = 2
         });
    
         // Assert
         result.IsSuccess.ShouldBeFalse();
     }
    [Fact]
    public async Task Add_LessonCorrectLessonType_ReturnsSuccess()
    {
        // Arrange
        var lessonList = new List<LessonType>
        {
            new()
            {
                Id = 1,
                Name = "Lecture"
            }
        };
        var mock = lessonList.AsQueryable().BuildMockDbSet();
        _dbContext.LessonTypes.Returns(mock);
    
        // Act
        var result = await _lessonService.AddAsync(new CreateLessonRequest
        {
            Code = "123",
            Name = "Math",
            Room = "A1",
            Date = DateTime.Now,
            LessonTypeId = 1
        });
    
        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
}