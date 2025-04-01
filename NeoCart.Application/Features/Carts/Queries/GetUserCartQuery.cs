using MediatR;
using NeoCart.Application.DTOs;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Carts.Queries;

public record GetUserCartQuery(Guid UserId, PaginationParams PaginationParams) : IRequest<IEnumerable<CartItem>>;