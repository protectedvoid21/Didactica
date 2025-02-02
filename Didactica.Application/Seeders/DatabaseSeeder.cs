using System.Text.Json;
using Bogus;
using Didactica.Domain.Models.Persistent;
using Didactica.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Didactica.Application.Seeders;

public class DatabaseSeeder
{
    private readonly DidacticaDbContext _dbContext;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly string _resourcesPath = Path.Combine("../", "Didactica.Application", "Seeders", "Resources");
    
    public DatabaseSeeder(DidacticaDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        var lessonTypes = await SeedLessonTypesAsync();
        var degrees = await SeedDegreesAsync();
        var teachersWithUsers = await SeedTeachersAsync(degrees);
        var lessons = await SeedLessonsAsync(lessonTypes, teachersWithUsers.teachers);
        var inspectionTeams = await SeedInspectionTeamsAsync(teachersWithUsers.teachers);
        var roles = await SeedRolesAsync();
        await SeedDeanAndWKJKAsync(roles, degrees, teachersWithUsers.users);
        await SeedInspectionsAsync(inspectionTeams, lessons, teachersWithUsers.teachers);
        
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
    
    private async Task<(List<Teacher> teachers, List<AppUser> users)> SeedTeachersAsync(List<Degree> degrees)
    {
        if(_dbContext.Teachers.Any())
        {
            return (_dbContext.Teachers.ToList(), _dbContext.Users.ToList());
        }
        
        var facultyList = await File.ReadAllLinesAsync(Path.Combine(_resourcesPath, "faculties.txt"));

        var userFaker = new Faker<AppUser>("pl")
            .RuleFor(u => u.UserName, f => f.Internet.UserName())
            .RuleFor(u => u.Email, f => f.Internet.Email());
        
        var users = userFaker.Generate(100);

        foreach (var user in users)
        {
            await _userManager.CreateAsync(user, "Test1234!");
        }
        
        var teacherFaker = new Faker<Teacher>("pl")
            .RuleFor(t => t.Name, f => f.Name.FirstName())
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
        
        return (teachers, users);
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

    private async Task<List<Inspection>> SeedInspectionsAsync(List<InspectionTeam> inspectionTeams, List<Lesson> lessons, List<Teacher> teachers)
    {
        if(_dbContext.Inspections.Any())
        {
            return _dbContext.Inspections.ToList();
        }
        
        var inspections = new List<Inspection>();
        
        var inspectionFaker = new Faker<Inspection>("pl")
            .RuleFor(i => i.CreatedOn, f => f.Date.Between(DateTime.Now.AddMonths(-2), DateTime.Now.AddMonths(2)))
            .RuleFor(i => i.IsRemote, f => f.Random.Bool(0.15f))
            .RuleFor(i => i.InspectionTeam, f => f.PickRandom(inspectionTeams))
            .RuleFor(i => i.Teacher, f => f.PickRandom(teachers))
            .RuleFor(i => i.Lesson, f => f.PickRandom(lessons));
        
        inspections.AddRange(inspectionFaker.Generate(100));
        
        inspections.ForEach(i =>
        {
            if (i.IsRemote)
            {
                i.LessonEnvironment = "Zdalnie";
            }
            else
            {
                i.LessonEnvironment = "Stacjonarnie";
            }
        });
        
        _dbContext.Inspections.AddRange(inspections);
        return inspections;
    }
    
    private async Task<List<IdentityRole<Guid>>> SeedRolesAsync()
    {
        if(_dbContext.Roles.Any())
        {
            return _dbContext.Roles.ToList();
        }
        
        var roles = new List<IdentityRole<Guid>>
        {
            new IdentityRole<Guid> {Name = "Admin"},
        };
        
        _dbContext.Roles.AddRange(roles);
        return roles;
    }

    private async Task SeedDeanAndWKJKAsync(List<IdentityRole<Guid>> roles, List<Degree> degrees, List<AppUser> users)
    {
        if (!roles.Any(r => r.Name == "Dean"))
        {
            await _roleManager.CreateAsync(new IdentityRole<Guid> { Name = "Dean" });
        }
        if (!roles.Any(r => r.Name == "WKJK"))
        {
            await _roleManager.CreateAsync(new IdentityRole<Guid> { Name = "WKJK" });
        }

        if(users.Any(u => u.UserName == "Dean" || u.UserName == "WKJK"))
        {
            return;
        }
        
        var dean = new AppUser
        {
            UserName = "Dean",
            Email = "dean@dean.com",
            Teacher = new Teacher
            {
                Name = "Zbigniew",
                LastName = "Kucharski",
                Email = "dean@dean.com",
                Faculty = "Wydział Informatyki i Telekomunikacji",
                PhoneNumber = "123456789",
                Degree = degrees.OrderBy(d => d.Id).Last(),
            }
        };

        var wkjk = new AppUser
        {
            UserName = "WKJK",
            Email = "wkjk@pwr.pl",
            Teacher = null,
        };
        
        await _userManager.CreateAsync(dean, "Dean1234!");
        await _userManager.CreateAsync(wkjk, "WKJK1234!");
        
        await _userManager.AddToRoleAsync(dean, "Dean");
        await _userManager.AddToRoleAsync(wkjk, "WKJK");
    }
}