using Didactica.Domain.Dto;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.Accounts.Register;

public record RegisterCommand(string UserName, string Email, string Password) : IRequest<Result<AuthTokenResponse>>;