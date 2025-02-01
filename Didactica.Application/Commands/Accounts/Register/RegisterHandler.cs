using Didactica.Application.Commands.Accounts.Login;
using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.Accounts.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, Result<AuthTokenResponse>>
{
    private readonly IAccountService _accountService;

    public RegisterHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<Result<AuthTokenResponse>> Handle(RegisterCommand request, CancellationToken ct)
    {
        return await _accountService.RegisterAsync(request.UserName, request.Email, request.Password, ct);
    }
}