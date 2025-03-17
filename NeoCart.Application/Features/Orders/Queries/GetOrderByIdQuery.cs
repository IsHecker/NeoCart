using MediatR;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Orders.Queries;

public record GetOrderByIdQuery(Guid Id) : IRequest<Order>;