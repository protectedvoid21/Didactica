using System.Reflection;
using Carter;
using Didactica.Application.Commands.Inspections;
using Didactica.Application.Commands.Inspections.Add;
using Didactica.Application.Services;
using Didactica.Domain.Services;
using Didactica.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddInspectionCommand).Assembly));
builder.Services.AddCarter();
builder.Services
    .AddScoped<IInspectionService, InspectionService>();

builder.Services.AddScoped<IDbContext, DidacticaDbContext>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IInspectionTeamService, InspectionTeamService>();
builder.Services.AddSerilog(config =>
{
    config
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
        .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
        .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
        .Enrich.FromLogContext();
});


builder.Services.AddNpgsql<DidacticaDbContext>(builder.Configuration.GetConnectionString("Didactica"), 
    options =>
    {
        options.MigrationsAssembly(typeof(DidacticaDbContext).Assembly);
    },
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
    app.Map("/", () => Results.Redirect("/swagger"));
}

app.MapCarter();

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
