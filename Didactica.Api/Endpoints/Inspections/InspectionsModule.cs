using System.Net;
using Carter;
using Didactica.Application.Commands;
using Didactica.Application.Commands.Inspections;
using Didactica.Application.Commands.Inspections.Add;
using Didactica.Application.Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Didactica.Api.Endpoints.Inspections;

public class InspectionsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("inspections");

        endpoints.MapPost("", async (IMediator mediator, AddInspectionCommand command) =>
        {
            var result = await mediator.Send(command);
            if (result.IsFailed)
            {
                return Results.BadRequest(result.ToApiResponse());
            }

            return Results.Created($"/inspections/{1}", result.ToApiResponse());
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

        endpoints.MapGet("/planned", async (IMediator mediator,[AsParameters] GetPlannedInspectionsQuery query) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result.ToApiResponse());
        });
    }
}