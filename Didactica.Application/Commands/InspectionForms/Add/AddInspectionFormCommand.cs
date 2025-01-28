using Didactica.Domain.Dto;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.InspectionForms.Submit;

public record AddInspectionFormCommand(AddInspectionFormRequest Request) : IRequest<Result>;