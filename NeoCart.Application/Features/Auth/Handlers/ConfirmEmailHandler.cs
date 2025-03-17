using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Auth.Commands;
using NeoCart.Application.Results;

namespace NeoCart.Application.Features.Auth.Handlers;

public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, ConfirmationResult>
{
    private readonly IAuthService _authService;

    public ConfirmEmailHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<ConfirmationResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        return await _authService.ConfirmEmailAsync(request.UserId, request.ConfirmationToken);
    }
}