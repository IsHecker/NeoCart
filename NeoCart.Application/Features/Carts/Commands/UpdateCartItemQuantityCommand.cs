using MediatR;

namespace NeoCart.Application.Features.Carts.Commands;

public record UpdateCartItemQuantityCommand(Guid Id, int Quantity) : IRequest;