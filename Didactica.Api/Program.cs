using System.Reflection;
using Carter;
using Didactica.Application.Services;
using Didactica.Domain.Services;
using Didactica.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Didactica.Api.Extensions;
using Didactica.Application.Commands.Inspections.Add;
using Didactica.Application.Seeders;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddInspectionCommand).Assembly));
builder.Services.AddCarter();
builder.Services.AddAuthentication(builder.Configuration, builder.Environment.IsProduction());

builder.Services
    .AddScoped<IDbContext, DidacticaDbContext>()
    .AddScoped<IPrivilegeService, PrivilegeService>()
    .AddScoped<IInspectionService, InspectionService>()
    .AddScoped<ITeacherService, TeacherService>()
    .AddScoped<ILessonService, LessonService>()
    .AddScoped<ITokenService, TokenService>()
    .AddScoped<IAccountService, AccountService>()
    .AddScoped<IInspectionTeamService, InspectionTeamService>();

builder.Services.AddScoped<DatabaseSeeder>();

builder.Services.Configure<SwaggerGeneratorOptions>(o => o.InferSecuritySchemes = true);


builder.Services.AddSerilog(config =>
{
    config
        .MinimumLevel.Debug()
        //.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
        .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
        .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
        .Enrich.FromLogContext();
});

builder.Services.AddNpgsql<DidacticaDbContext>(builder.Configuration.GetConnectionString("Didactica"),
    options => { options.MigrationsAssembly(typeof(DidacticaDbContext).Assembly); },
    dbBuilder =>
    {
        dbBuilder.UseSnakeCaseNamingConvention();
        if (builder.Environment.IsDevelopment())
        {
            dbBuilder.EnableSensitiveDataLogging();
        }
    }
);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        config =>
        {
            config
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
await seeder.SeedAsync();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Map("/", () => Results.Redirect("/swagger")).AllowAnonymous();
}

app.MapCarter();

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();