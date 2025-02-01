using Carter;
using Didactica.Application.Commands.Accounts.Login;
using Didactica.Application.Commands.Accounts.Register;
using Didactica.Application.Common.Extensions;
using Didactica.Application.Common.Models;
using Didactica.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Didactica.Api.Endpoints;

public class AccountsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("accounts");

        endpoints.MapPost("/login", async (IMediator mediator, [FromBody] LoginCommand command) =>
        {
            var result = await mediator.Send(command);
            return result.IsFailed 
                ? Results.BadRequest(result.ToApiResponse()) 
                : Results.Ok(result.ToApiResponse());
        })
        .Produces<ApiResponse<AuthTokenResponse>>()
        .Produces<ApiResponse<AuthTokenResponse>>(StatusCodes.Status400BadRequest);

        endpoints.MapPost("/register", async (IMediator mediator, [FromBody] RegisterCommand command) =>
        {
            var result = await mediator.Send(command);
            if (result.IsFailed)
            {
                return Results.BadRequest(result.ToApiResponse());
            }

            return Results.NoContent();
        })
        .Produces<ApiResponse<AuthTokenResponse>>()
        .Produces<ApiResponse<AuthTokenResponse>>(StatusCodes.Status400BadRequest);;
    }
}