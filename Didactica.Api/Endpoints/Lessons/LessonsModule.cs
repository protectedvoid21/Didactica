using Carter;
using Didactica.Application.Commands.Inspections;
using Didactica.Application.Commands.Lessons;
using Didactica.Application.Common.Extensions;
using MediatR;

namespace Didactica.Api.Endpoints.Lessons;

/// <summary>
/// The LessonsModule class defines HTTP endpoints for managing lessons in the system.
/// It implements the ICarterModule interface to configure routing and handling of
/// incoming requests related to lessons.
/// </summary>
public class LessonsModule : ICarterModule
{
    /// <summary>
    /// Configures endpoint routes for the lessons module in the API.
    /// </summary>
    /// <param name="app">An instance of <see cref="IEndpointRouteBuilder"/> used to define routes and endpoints.</param>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("lessons");

        endpoints.MapGet("", async (IMediator mediator, [AsParameters] GetLessonsQuery query) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result.ToApiResponse());
        });

        endpoints.MapPost("", async (IMediator mediator, AddLessonCommand command) =>
        {
            var result = await mediator.Send(command);
            if (result.IsFailed)
            {
                return Results.BadRequest(result.ToApiResponse());
            }

            return Results.Created($"/inspections/{2137}", result.ToApiResponse());
        });
    }
}