using MediatR;
using NeoCart.Application.DTOs;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Orders.Queries;

public record GetUserOrdersQuery(Guid UserId, bool IncludeOrderItems, PaginationParams PaginationParams) : IRequest<IEnumerable<Order>>;