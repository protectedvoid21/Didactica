using Carter;
using Didactica.Api.Extensions;
using Didactica.Application.Commands.InspectionForms.Add;
using Didactica.Application.Common.Extensions;
using MediatR;

namespace Didactica.Api.Endpoints;

/// <summary>
/// Represents a module responsible for defining endpoints related to inspection forms.
/// This class implements the <see cref="ICarterModule"/> interface, which allows
/// configuration of HTTP routes for the inspection forms functionality.
/// </summary>
public class InspectionFormsModule : ICarterModule
{
	/// Adds the routes for the Inspection Forms module to the application endpoint route builder.
	/// <param name="app">
	/// The endpoint route builder used to define route mappings for the application.
	/// </param>
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		var endpoints = app.MapGroup("inspection-forms");
		endpoints.AddOpenApiSecurityRequirement();

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
