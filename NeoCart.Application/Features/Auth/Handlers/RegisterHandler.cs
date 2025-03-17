using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Auth.Commands;
using NeoCart.Application.Results;

namespace NeoCart.Application.Features.Auth.Handlers;

public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResult>
{
    private readonly IAuthService _authService;

    public RegisterHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authService.RegisterAsync(request.RegisterDto);
    }
}