using Carter;
using Didactica.Application.Commands.InspectionTeam;
using Didactica.Application.Common.Extensions;
using MediatR;

namespace Didactica.Api.Endpoints.InspectionTeams;

public class InspectionTeamsModule : ICarterModule
{
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

                return Results.Created($"/inspectionTeams/{1}", result.ToApiResponse());
            });
        }
    }
}
    