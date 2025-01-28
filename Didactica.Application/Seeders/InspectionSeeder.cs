using Bogus;
using Didactica.Domain.Models;
using Didactica.Infrastructure;

namespace Didactica.Application.Seeders;

public class InspectionSeeder : ISeeder
{
    private readonly DidacticaDbContext _context;

    public InspectionSeeder(DidacticaDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        var teachersFaker = new Faker<Teacher>()
            .RuleFor(t => t.Name, f => f.Person.FirstName)
            .RuleFor(t => t.LastName, f => f.Person.LastName)
            .RuleFor(t => t.Email, f => f.Person.Email)
            .RuleFor(t => t.PhoneNumber, f => f.Person.Phone);

    }
}