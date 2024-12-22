using Didactica.Api.Persistence;
using Didactica.Api.Service;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddSerilog(config =>
{
    config
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
        .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
        .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
        .Enrich.FromLogContext();
});


builder.Services.AddNpgsql<DidacticaDbContext>(builder.Configuration.GetConnectionString("Didactica"), null,
    dbBuilder =>
    {
        dbBuilder.UseSnakeCaseNamingConvention();
        if (builder.Environment.IsDevelopment())
        {
            dbBuilder.EnableSensitiveDataLogging();
        }
    }
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

