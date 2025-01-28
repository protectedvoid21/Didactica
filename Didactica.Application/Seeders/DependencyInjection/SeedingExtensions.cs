using Microsoft.Extensions.DependencyInjection;

namespace Didactica.Application.Seeders.DependencyInjection;

public static class SeedingExtensions
{
    public static IServiceCollection AddDatabaseSeeding(this IServiceCollection services)
    {
        var seeders = typeof(SeedingExtensions).Assembly.GetTypes()
            .Where(t => t.GetInterfaces().Contains(typeof(ISeeder)) && !t.IsAbstract)
            .ToArray();
        
        services.AddTransient<ISeeder>(provider => new DidacticaSeeder(provider.GetServices<ISeeder>().ToArray()));

        return services;
    }
}