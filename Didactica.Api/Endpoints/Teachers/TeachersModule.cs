using Carter;
using Didactica.Application.Commands.Teachers;
using Didactica.Application.Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Didactica.Api.Endpoints.Teachers;

public class TeachersModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("teachers");

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