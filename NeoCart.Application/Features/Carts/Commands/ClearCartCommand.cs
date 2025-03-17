using MediatR;

namespace NeoCart.Application.Features.Carts.Commands;

public record ClearCartCommand(Guid UserId) : IRequest;