using MediatR;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Carts.Commands;

public record AddCartItemCommand(CartItem CartItem) : IRequest<CartItem?>;