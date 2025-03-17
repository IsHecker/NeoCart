using MediatR;
using NeoCart.Application.Results;

namespace NeoCart.Application.Features.Auth.Commands;

public record ConfirmEmailCommand(string UserId, string ConfirmationToken) : IRequest<ConfirmationResult>;