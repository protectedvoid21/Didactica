namespace Didactica.Application.Seeders;

public class DidacticaSeeder : ISeeder
{
    private readonly ISeeder[] _seeders;

    public DidacticaSeeder(ISeeder[] seeders)
    {
        _seeders = seeders;
    }

    public async Task SeedAsync()
    {
        foreach(var seeder in _seeders)
        {
            await seeder.SeedAsync();
        }
    }
}