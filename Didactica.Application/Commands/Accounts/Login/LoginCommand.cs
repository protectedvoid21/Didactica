using Didactica.Domain.Dto;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.Accounts.Login;

public record LoginCommand(string UserName, string Password) : IRequest<Result<AuthTokenResponse>>;