using Carter;
using Didactica.Api.Extensions;
using Didactica.Application.Commands.InspectionTeam;
using Didactica.Application.Common.Extensions;
using Didactica.Application.Common.Models;
using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Didactica.Api.Endpoints;

/// <summary>
/// The InspectionTeamsModule class is responsible for defining API routes related to
/// inspection team functionalities within the application.
/// It uses Carter for lightweight routing and MediatR for handling commands.
/// </summary>
public class InspectionTeamsModule : ICarterModule
{
    /// <summary>
    /// Defines endpoint routes for inspection teams functionality.
    /// </summary>
    /// <param name="app">The <see cref="IEndpointRouteBuilder"/> used to define application endpoint routes.</param>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("inspection-teams").WithTags("Inspection Teams");
        endpoints.AddOpenApiSecurityRequirement();
        
        endpoints.MapPost("", async (IMediator mediator, AddInspectionTeamCommand command) =>
        {
            var result = await mediator.Send(command);
            if (result.IsFailed)
            {
                return Results.BadRequest(result.ToApiResponse());
            }

            return Results.Created("/inspectionTeams/{1}"/*Placeholder*/, result.ToApiResponse());
        });

        endpoints.MapGet("/", async (
            IMediator mediator,
            [FromServices] CurrentUser user,
            [FromServices] IPrivilegeService privilegeService) =>
        {
            if (!await privilegeService.IsUserInRoleAsync(user.Id, "WKJK"))
            {
                return Results.Forbid();
            }
            
            var result = await mediator.Send(new GetInspectionsTeamsQuery());
            return Results.Ok(result.ToApiResponse());
        }).Produces<ApiResponse<IEnumerable<GetInspectionTeamResponse>>>();

        endpoints.MapGet("/inspections", async (
            IMediator mediator,
            [FromServices] CurrentUser user
            ) =>
        {
            if (user.User.TeacherId == null)
            {
                return Results.BadRequest(new ApiResponse
                {
                    Message = ["User is not a teacher."],
                    IsSuccess = false
                });
            }
            
            var result = await mediator.Send(new GetInspectionsForTeamTeacherQuery(user.User.TeacherId.Value));
            return Results.Ok(result.ToApiResponse());
        }).Produces<ApiResponse<IEnumerable<GetInspectionResponse>>>()
        .Produces<ApiResponse<IEnumerable<GetInspectionResponse>>>(StatusCodes.Status400BadRequest);
    }
}
    