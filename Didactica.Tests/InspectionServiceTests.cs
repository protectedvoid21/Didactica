using Shouldly;
using Didactica.Application.Services;
using Didactica.Domain.Dto;
using Didactica.Domain.Models.Persistent;
using Didactica.Domain.Services;
using MockQueryable.NSubstitute;
using NSubstitute;
using Shouldly;

namespace Didactica.Tests;

public class InspectionServiceTests
{
    private readonly IDbContext _dbContext = Substitute.For<IDbContext>();
    private readonly InspectionService _inspectionService;
    
    public InspectionServiceTests()
    {
        _inspectionService = new InspectionService(_dbContext);
    }

    [Fact]
    public async Task Get_InspectionNotFound_ReturnsFailure()
    {
        // Arrange
        var inspections = new List<Inspection>().AsQueryable().BuildMockDbSet();
        _dbContext.Inspections.Returns(inspections);

        // Act
        var result = await _inspectionService.GetAsync(1);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.Message == "Inspection not found");
    }

    [Fact]
    public async Task Get_InspectionFound_ReturnsSuccess()
    {
        // Arrange
        var inspection = new Inspection
        {
            Id = 1,
            Teacher = new Teacher { Id = 1, Name = "John", LastName = "Doe" },
            Lesson = new Lesson { Name = "Math", LessonType = new LessonType { Name = "Lecture" }, Date = DateTime.UtcNow, Code = "123123"},
            IsRemote = false,
            LessonEnvironment = "Classroom",
            InspectionTeam = new InspectionTeam { Id = 1, Teachers = new List<Teacher>() }
        };
        var inspections = new List<Inspection> { inspection }.AsQueryable().BuildMockDbSet();
        _dbContext.Inspections.Returns(inspections);

        // Act
        var result = await _inspectionService.GetAsync(1);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Id.ShouldBe(1);
    }

    [Fact]
    public async Task Add_InspectionTeacherNotFound_ReturnsFailure()
    {
        // Arrange
        var teachers = new List<Teacher>().AsQueryable().BuildMockDbSet();
        _dbContext.Teachers.Returns(teachers);

        // Act
        var result = await _inspectionService.AddAsync(new CreateInspectionRequest
        {
            TeacherId = 1,
            LessonId = 1,
            IsRemote = false,
            LessonEnvironment = "Classroom",
            InspectionMethodId = 1
        });

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.Message == "Teacher not found");
    }

    [Fact]
    public async Task Add_InspectionLessonNotFound_ReturnsFailure()
    {
        // Arrange
        var teachers = new List<Teacher> { new() { Id = 1, Name = "John", LastName = "Adams"} }.AsQueryable().BuildMockDbSet();
        var lessons = new List<Lesson>().AsQueryable().BuildMockDbSet();
        _dbContext.Teachers.Returns(teachers);
        _dbContext.Lessons.Returns(lessons);

        // Act
        var result = await _inspectionService.AddAsync(new CreateInspectionRequest
        {
            TeacherId = 1,
            LessonId = 1,
            IsRemote = false,
            LessonEnvironment = "Classroom",
            InspectionMethodId = 1
        });

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.Message == "Lesson not found");
    }

    [Fact]
    public async Task Add_InspectionSuccessful_ReturnsSuccess()
    {
        // Arrange
        var teachers = new List<Teacher> { new() { Id = 1, Name = "John", LastName = "Adams"} }.AsQueryable().BuildMockDbSet();
        var lessons = new List<Lesson> { new() { 
            Id = 1,
            Name = "Math",
            LessonType = new LessonType { Name = "Lecture" },
            Date = DateTime.UtcNow,
            Code = "123123" } }.AsQueryable().BuildMockDbSet();
        _dbContext.Teachers.Returns(teachers);
        _dbContext.Lessons.Returns(lessons);

        // Act
        var result = await _inspectionService.AddAsync(new CreateInspectionRequest
        {
            TeacherId = 1,
            LessonId = 1,
            IsRemote = false,
            LessonEnvironment = "Classroom",
            InspectionMethodId = 1
        });

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task Delete_InspectionNotFound_ReturnsFailure()
    {
        // Arrange
        var inspections = new List<Inspection>().AsQueryable().BuildMockDbSet();
        _dbContext.Inspections.Returns(inspections);

        // Act
        var result = await _inspectionService.DeleteAsync(new DeleteInspectionRequest { InspectionId = 1 });

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.Message == "Inspection not found");
    }

    [Fact]
    public async Task Delete_InspectionFound_ReturnsSuccess()
    {
        // Arrange
        var inspection = new Inspection
        {
            Id = 1,
            Teacher = new Teacher { Id = 1, Name = "John", LastName = "Doe" },
            Lesson = new Lesson
            {
                Id = 1,
                Name = "Math",
                LessonType = new LessonType { Name = "Lecture" }, Date = DateTime.UtcNow,
                Code = "123123"
            },
            IsRemote = false,
            LessonEnvironment = "Classroom"
        };

        var inspections = new List<Inspection> { inspection }.AsQueryable().BuildMockDbSet();
        _dbContext.Inspections.Returns(inspections);

        // Act
        var result = await _inspectionService.DeleteAsync(new DeleteInspectionRequest { InspectionId = 1 });

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
    [Fact]
public async Task Get_AllPlannedInspections_ReturnsSuccess()
{
    // Arrange
    var inspections = new List<Inspection>
    {
        new()
        {
            Id = 1,
            Teacher = new Teacher { Id = 1, Name = "John", LastName = "Doe" },
            Lesson = new Lesson { Name = "Math", LessonType = new LessonType { Name = "Lecture" }, Date = DateTime.UtcNow.AddDays(1), Code = "123123" },
            IsRemote = false,
            LessonEnvironment = "Classroom",
            InspectionTeam = new InspectionTeam
            {
                Id = 1,
                Teachers = new List<Teacher>
                {
                    new Teacher { Id = 2, Name = "Jane", LastName = "Smith" },
                    new Teacher { Id = 3, Name = "Alice", LastName = "Brown" }
                }
            }
        },
        new()
        {
            Id = 2,
            Teacher = new Teacher { Id = 3, Name = "Alice", LastName = "Brown" },
            Lesson = new Lesson { Name = "Physics", LessonType = new LessonType { Name = "Lecture" }, Date = DateTime.UtcNow.AddDays(2), Code = "123123" },
            IsRemote = true,
            LessonEnvironment = "Online",
            InspectionTeam = null
        }
    };
    var mock = inspections.AsQueryable().BuildMockDbSet();
    _dbContext.Inspections.Returns(mock);

    // Act
    var result = await _inspectionService.GetAllPlannedInspections();

    // Assert
    result.IsSuccess.ShouldBeTrue();
    result.Value.ShouldNotBeEmpty();
    result.Value.Count().ShouldBe(2);
    result.Value.First().TeacherFirstName.ShouldBe("John");
    result.Value.Last().TeacherFirstName.ShouldBe("Alice");
}

[Fact]
public async Task Get_AllPlannedInspections_NoPlannedInspections_ReturnsEmpty()
{
    // Arrange
    var inspections = new List<Inspection>().AsQueryable().BuildMockDbSet();
    _dbContext.Inspections.Returns(inspections);

    // Act
    var result = await _inspectionService.GetAllPlannedInspections();

    // Assert
    result.IsSuccess.ShouldBeTrue();
    result.Value.ShouldBeEmpty();
}

[Fact]
public async Task Get_AllPlannedInspections_InspectionsInPast_ReturnsEmpty()
{
    // Arrange
    var inspections = new List<Inspection>
    {
        new()
        {
            Id = 1,
            Teacher = new Teacher { Id = 1, Name = "John", LastName = "Doe" },
            Lesson = new Lesson { Name = "Math", LessonType = new LessonType { Name = "Lecture" }, Date = DateTime.UtcNow.AddDays(-1), Code = "123123" },
            IsRemote = false,
            LessonEnvironment = "Classroom",
            InspectionTeam = new InspectionTeam { Id = 1, Teachers = new List<Teacher>() }
        }
    }.AsQueryable().BuildMockDbSet();
    _dbContext.Inspections.Returns(inspections);

    // Act
    var result = await _inspectionService.GetAllPlannedInspections();

    // Assert
    result.IsSuccess.ShouldBeTrue();
    result.Value.ShouldBeEmpty();
}

[Fact]
public async Task Get_AllPlannedInspections_NullInspectionTeam_ReturnsSuccess()
{
    // Arrange
    var inspections = new List<Inspection>
    {
        new()
        {
            Id = 1,
            Teacher = new Teacher { Id = 1, Name = "John", LastName = "Doe" },
            Lesson = new Lesson { Name = "Math", LessonType = new LessonType { Name = "Lecture" }, Date = DateTime.UtcNow.AddDays(1), Code = "123123" },
            IsRemote = false,
            LessonEnvironment = "Classroom",
            InspectionTeam = null
        }
    }.AsQueryable().BuildMockDbSet();
    _dbContext.Inspections.Returns(inspections);

    // Act
    var result = await _inspectionService.GetAllPlannedInspections();

    // Assert
    result.IsSuccess.ShouldBeTrue();
    result.Value.ShouldNotBeEmpty();
    result.Value.Count().ShouldBe(1);
    result.Value.First().TeacherFirstName.ShouldBe("John");
    result.Value.First().GetInspectionTeamResponse.ShouldBeNull();
}

    [Fact]
    public async Task GetInspectionsOfTeacherById_TeacherHasNoInspections_ReturnsEmpty()
    {
        // Arrange
        var inspections = new List<Inspection>().AsQueryable().BuildMockDbSet();
        _dbContext.Inspections.Returns(inspections);

        // Act
        var result = await _inspectionService.GetInspectionsOfTeacherById(1);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEmpty();
    }

    [Fact]
    public async Task GetInspectionsOfTeacherById_TeacherHasInspections_ReturnsCorrectData()
    {
        // Arrange
        var teacher = new Teacher { Id = 1, Name = "John", LastName = "Doe" };
        var lesson = new Lesson { Id = 1, Name = "Math", LessonType = new LessonType { Name = "Lecture" }, Date = DateTime.UtcNow, Room = "101",Code = "123123" };
        var inspectionTeam = new InspectionTeam { Id = 1, Teachers = new List<Teacher> { teacher } };
        var inspections = new List<Inspection>
        {
            new()
            {
                Id = 1,
                Teacher = teacher,
                Lesson = lesson,
                IsRemote = false,
                LessonEnvironment = "Classroom",
                InspectionTeam = inspectionTeam
            }
        }.AsQueryable().BuildMockDbSet();
        _dbContext.Inspections.Returns(inspections);

        // Act
        var result = await _inspectionService.GetInspectionsOfTeacherById(1);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeEmpty();
        result.Value.Count().ShouldBe(1);
        result.Value.First().TeacherFirstName.ShouldBe("John");
        result.Value.First().Course.ShouldBe("Math");
    }

    [Fact]
    public async Task GetInspectionsOfTeacherById_TeacherIdNotFound_ReturnsEmpty()
    {
        // Arrange
        var teacher = new Teacher { Id = 2, Name = "Jane", LastName = "Smith" };
        var lesson = new Lesson { Id = 1, Name = "Physics", LessonType = new LessonType { Name = "Lab" }, Date = DateTime.UtcNow, Room = "202", Code = "123123"};
        var inspection = new Inspection
        {
            Id = 1,
            Teacher = teacher,
            Lesson = lesson,
            IsRemote = true,
            LessonEnvironment = "Online",
            InspectionTeam = new InspectionTeam { Id = 1, Teachers = new List<Teacher> { teacher } }
        };

        var inspections = new List<Inspection> { inspection }.AsQueryable().BuildMockDbSet();
        _dbContext.Inspections.Returns(inspections);

        // Act
        var result = await _inspectionService.GetInspectionsOfTeacherById(1);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEmpty();
    }
}
