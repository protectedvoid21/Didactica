using System.Net;
using Carter;
using Didactica.Application.Commands;
using Didactica.Application.Commands.Inspections;
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

            return Results.Created($"/inspections/{2137}", result.ToApiResponse()); // 2137 is a placeholder
        });
        
        endpoints.MapGet("{id}", async (IMediator mediator, [AsParameters] GetInspectionQuery query) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result.ToApiResponse());
        });
    }
}