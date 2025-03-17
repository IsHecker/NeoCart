using MediatR;

namespace NeoCart.Application.Features.Carts.Commands;

public record RemoveCartItemCommand(Guid Id) : IRequest;