using Carter;
using Didactica.Application.Commands.InspectionForms.Submit;
using Didactica.Application.Common.Extensions;
using MediatR;

namespace Didactica.Api.Endpoints.Inspections;

public class InspectionFormsModule : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		var endpoints = app.MapGroup("inspection-forms");

		endpoints.MapPost("", async (IMediator mediator, AddInspectionFormCommand command) =>
		{
			var result = await mediator.Send(command);
			if (result.IsFailed)
			{
				return Results.BadRequest(result.ToApiResponse());
			}

			return Results.Created($"/inspection-forms/{2137}", result.ToApiResponse()); // 2137 is a placeholder
		});
	}
}
