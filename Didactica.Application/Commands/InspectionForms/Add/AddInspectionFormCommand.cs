using Didactica.Domain.Dto;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.InspectionForms.Add;

public record AddInspectionFormCommand(int InspectionId, AddInspectionFormRequest Request) : IRequest<Result>;