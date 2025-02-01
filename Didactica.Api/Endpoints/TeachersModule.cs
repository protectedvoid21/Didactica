using Carter;
using Didactica.Api.Extensions;
using Didactica.Application.Commands.Teachers;
using Didactica.Application.Common.Extensions;
using MediatR;

namespace Didactica.Api.Endpoints;

/// <summary>
/// Represents a module for managing teacher-related functionalities within an educational system.
/// Encapsulates operations and attributes pertinent to teachers, such as their information,
/// assignments, and data management specific to the teacher's role.
/// </summary>
public class TeachersModule : ICarterModule
{
    /// <summary>
    /// Adds multiple routes to the routing table.
    /// </summary>
    /// <param name="routes">A collection of routes to add.</param>
    /// <param name="overwriteExisting">
    /// A boolean value indicating whether existing routes with the same name should be overwritten.
    /// </param>
    /// <param name="routeValidator">
    /// A delegate used to validate the routes before adding them.
    /// Returns true if the route is valid, otherwise false.
    /// </param>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("teachers");
        endpoints.AddOpenApiSecurityRequirement();

        endpoints.MapGet("", async (IMediator mediator, [AsParameters] GetTeachersQuery query) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result.ToApiResponse());
        });
        
        endpoints.MapGet("/{id}", async (IMediator mediator, [AsParameters] GetTeacherQuery query) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result.ToApiResponse());
        });
    }
}