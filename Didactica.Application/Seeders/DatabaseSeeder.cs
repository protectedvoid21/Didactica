using System.Text.Json;
using Bogus;
using Didactica.Domain.Models.Persistent;
using Didactica.Infrastructure;

namespace Didactica.Application.Seeders;

public class DatabaseSeeder
{
    private readonly DidacticaDbContext _dbContext;
    private readonly string _resourcesPath = Path.Combine("../", "Didactica.Application", "Seeders", "Resources");
    
    public DatabaseSeeder(DidacticaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        var lessonTypes = await SeedLessonTypesAsync();
        var degrees = await SeedDegreesAsync();
        var teachers = await SeedTeachersAsync(degrees);
        var lessons = await SeedLessonsAsync(lessonTypes, teachers);
        var inspectionTeams = await SeedInspectionTeamsAsync(teachers);
        
        await _dbContext.SaveChangesAsync();
    }

    private async Task<List<Degree>> SeedDegreesAsync()
    {
        if(_dbContext.Degrees.Any())
        {
            return _dbContext.Degrees.ToList();
        }

        string degreeJsonText = await File.ReadAllTextAsync(Path.Combine(_resourcesPath, "degrees.json"));
        var degrees = JsonSerializer.Deserialize<List<Degree>>(degreeJsonText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        _dbContext.Degrees.AddRange(degrees);
        return degrees;
    }

    private async Task<List<LessonType>> SeedLessonTypesAsync()
    {
        if(_dbContext.LessonTypes.Any())
        {
            return _dbContext.LessonTypes.ToList();
        }
        
        var lessonTypes = new List<LessonType>
        {
            new LessonType {Name = "Wykład"},
            new LessonType {Name = "Ćwiczenia"},
            new LessonType {Name = "Laboratorium"},
            new LessonType {Name = "Projekt"},
            new LessonType {Name = "Seminarium"}
        };
        _dbContext.LessonTypes.AddRange(lessonTypes);
        return lessonTypes;
    }
    
    private async Task<List<Teacher>> SeedTeachersAsync(List<Degree> degrees)
    {
        if(_dbContext.Teachers.Any())
        {
            return _dbContext.Teachers.ToList();
        }
        
        var facultyList = await File.ReadAllLinesAsync(Path.Combine(_resourcesPath, "faculties.txt"));
        
        var userFaker = new Faker<AppUser>("pl")
            .RuleFor(u => u.UserName, f => f.Internet.UserName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.PasswordHash, f => f.Internet.Password());
        
        var users = userFaker.Generate(100);
        
        _dbContext.Users.AddRange(users);

        var teacherFaker = new Faker<Teacher>("pl")
            .RuleFor(t => t.Name, f => f.Name.FullName())
            .RuleFor(t => t.LastName, f => f.Name.LastName())
            .RuleFor(t => t.Email, f => f.Internet.Email())
            .RuleFor(t => t.Faculty, f => f.PickRandom(facultyList))
            .RuleFor(t => t.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(t => t.Degree, f => f.PickRandom(degrees));
        
        var teachers = new List<Teacher>();
        
        foreach(var user in users)
        {
            var teacher = teacherFaker.Generate();
            teachers.Add(teacher);
            user.Teacher = teacher;
            _dbContext.Teachers.Add(teacher);
        }
        
        return teachers;
    }
    
    private async Task<List<Lesson>> SeedLessonsAsync(List<LessonType> lessonTypes, List<Teacher> teachers)
    {
        if(_dbContext.Lessons.Any())
        {
            return _dbContext.Lessons.ToList();
        }
        
        var coursesNames = await File.ReadAllLinesAsync(Path.Combine(_resourcesPath, "courses.txt"));

        var lessonFaker = new Faker<Lesson>("pl")
            .RuleFor(l => l.Code, f => f.Random.AlphaNumeric(5))
            .RuleFor(l => l.Name, f => f.PickRandom(coursesNames))
            .RuleFor(l => l.Date, f => f.Date.Between(DateTime.Now.AddMonths(-2), DateTime.Now.AddMonths(2)))
            .RuleFor(l => l.LessonType, f => f.PickRandom(lessonTypes))
            .RuleFor(l => l.Room, f => f.Random.Number(1, 500).ToString() + f.Random.Char('a', 'h'));
        
        var lessons = lessonFaker.Generate(100);
        
        _dbContext.Lessons.AddRange(lessons);
        return lessons;
    }

    private async Task<List<InspectionTeam>> SeedInspectionTeamsAsync(List<Teacher> teachers)
    {
        if(_dbContext.InspectionTeams.Any())
        {
            return _dbContext.InspectionTeams.ToList();
        }

        var inspectionTeamFaker = new Faker<InspectionTeam>("pl")
            .RuleFor(it => it.Teachers, f => f.PickRandom(teachers, f.Random.Number(3, 4)).ToList());
        
        var inspectionTeams = inspectionTeamFaker.Generate(10);
        
        _dbContext.InspectionTeams.AddRange(inspectionTeams);
        return inspectionTeams;
    }
}