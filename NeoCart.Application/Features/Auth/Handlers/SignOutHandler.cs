using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Auth.Commands;

namespace NeoCart.Application.Features.Auth.Handlers;

public class SignOutHandler : IRequestHandler<SignOutCommand>
{
    private readonly IAuthService _authService;

    public SignOutHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        await _authService.SignOutAsync();
    }
}