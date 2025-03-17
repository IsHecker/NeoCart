using MediatR;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Carts.Queries;

public record GetUserCartQuery(Guid UserId) : IRequest<IEnumerable<CartItem>>;