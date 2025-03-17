using MediatR;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Orders.Queries;

public record GetUserOrdersQuery(Guid UserId) : IRequest<IEnumerable<Order>>;