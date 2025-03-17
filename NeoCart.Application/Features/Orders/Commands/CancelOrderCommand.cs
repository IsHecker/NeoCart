using MediatR;

namespace NeoCart.Application.Features.Orders.Commands;

public record CancelOrderCommand(Guid Id) : IRequest;