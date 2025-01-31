using Didactica.Domain.Dto;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.InspectionForms.Add;

public record AddInspectionFormCommand(AddInspectionFormRequest Request) : IRequest<Result>;