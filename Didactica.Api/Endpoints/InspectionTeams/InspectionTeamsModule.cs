using Carter;
using Didactica.Application.Commands.InspectionTeam;
using Didactica.Application.Common.Extensions;
using MediatR;

namespace Didactica.Api.Endpoints.InspectionTeams;

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
        var endpoints = app.MapGroup("inspectionTeams");
        {
            endpoints.MapPost("", async (IMediator mediator, AddInspectionTeamCommand command) =>
            {
                var result = await mediator.Send(command);
                if (result.IsFailed)
                {
                    return Results.BadRequest(result.ToApiResponse());
                }

                return Results.Created("/inspectionTeams/{1}"/*Placeholder*/, result.ToApiResponse());
            });
        }
    }
}
    