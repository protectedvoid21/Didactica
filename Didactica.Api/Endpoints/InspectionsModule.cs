using Carter;
using Didactica.Api.Extensions;
using Didactica.Application.Commands.Inspections;
using Didactica.Application.Commands.Inspections.Add;
using Didactica.Application.Common.Extensions;
using Didactica.Application.Common.Models;
using Didactica.Application.Utils;
using Didactica.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Didactica.Api.Endpoints;

/// <summary>
/// Defines and configures HTTP endpoints for inspection-related operations.
/// Handles routes for adding, deleting, and retrieving inspection data tied to teachers or planned inspections.
/// </summary>
public class InspectionsModule : ICarterModule
{
    /// <summary>
    /// Configures the routes for inspection-related API endpoints and maps them to their respective handlers.
    /// </summary>
    /// <param name="app">The IEndpointRouteBuilder used to define and build the application endpoints.</param>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("inspections")
            .WithTags("Inspections");
        endpoints.AddOpenApiSecurityRequirement();

        endpoints.MapPost("", async (IMediator mediator, AddInspectionCommand command) =>
        {
            var result = await mediator.Send(command);
            if (result.IsFailed)
            {
                return Results.BadRequest(result.ToApiResponse());
            }

            return Results.Created("/inspections/{1}"/*Placeholder*/, result.ToApiResponse());
        });
        
        endpoints.MapDelete("{id}", async (IMediator mediator, [FromBody] DeleteInspectionCommand command) =>
        {
            var result = await mediator.Send(command);
            if (result.IsFailed)
            {
                return Results.BadRequest(result.ToApiResponse());
            }

            return Results.NoContent();
        });

        endpoints.MapGet("/teachers/{teacherId}", async (IMediator mediator, [AsParameters] GetInspectionsOfTeacherQuery query) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result.ToApiResponse());
        });
        
        endpoints.MapGet("/planned", async (
            IMediator mediator, 
            [FromServices] CurrentUser user, 
            [FromServices] IPrivilegeService privilegeService,
            [AsParameters] GetPlannedInspectionsQuery query) =>
        {
            if (await privilegeService.IsUserInRoleAsync(user.Id, "Dean"))
            {
                return Results.Forbid();
            }
            var result = await mediator.Send(query);
            return Results.Ok(result.ToApiResponse());
        });
    }
}