using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Auth.Commands;
using NeoCart.Application.Results;

namespace NeoCart.Application.Features.Auth.Handlers;

public class SignInHandler : IRequestHandler<SignInCommand, SignInResult>
{
    private readonly IAuthService _authService;

    public SignInHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<SignInResult> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        return await _authService.SignInAsync(request.SignInDto);
    }
}