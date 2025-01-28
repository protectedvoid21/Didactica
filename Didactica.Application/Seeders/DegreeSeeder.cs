using Didactica.Domain.Models;
using Didactica.Infrastructure;

namespace Didactica.Application.Seeders;

public class DegreeSeeder : ISeeder
{
    private readonly DidacticaDbContext _dbContext;

    public DegreeSeeder(DidacticaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task SeedAsync()
    {
        if (_dbContext.Degrees.Any())
        {
            return;
        }

        var degrees = new[]
        {
            new Degree { Name = "inżynier", Short = "inż." },
            new Degree { Name = "magister", Short = "mgr" },
            new Degree { Name = "magister inżynier", Short = "mgr inż." },
            new Degree { Name = "doktor", Short = "dr" },
            new Degree { Name = "doktor inżynier", Short = "dr inż." },
            new Degree { Name = "doktor habilitowany", Short = "dr hab." },
            new Degree { Name = "doktor habilitowany inżynier", Short = "dr hab. inż." },
            new Degree { Name = "profesor doktor habilitowany", Short = "prof. dr hab." },
            new Degree { Name = "profesor doktor habilitowany inżynier", Short = "prof. dr hab. inż." },
        };

        _dbContext.AddRange(degrees);
        await _dbContext.SaveChangesAsync();
    }
}