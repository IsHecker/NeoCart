using MediatR;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Orders.Commands;

public record CheckoutCommand(Guid UserId) : IRequest<Order>;