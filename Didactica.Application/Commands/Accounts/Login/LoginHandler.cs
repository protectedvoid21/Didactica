using Didactica.Domain.Dto;
using Didactica.Domain.Services;
using FluentResults;
using MediatR;

namespace Didactica.Application.Commands.Accounts.Login;

public class LoginHandler : IRequestHandler<LoginCommand, Result<AuthTokenResponse>>
{
    private readonly IAccountService _accountService;

    public LoginHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<Result<AuthTokenResponse>> Handle(LoginCommand request, CancellationToken ct)
    {
        return await _accountService.LoginAsync(request.UserName, request.Password, ct);
    }
}