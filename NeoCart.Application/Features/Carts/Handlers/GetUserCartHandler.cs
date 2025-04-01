using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Carts.Queries;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Carts.Handlers;

public class GetUserCartHandler : IRequestHandler<GetUserCartQuery, IEnumerable<CartItem>>
{
    private readonly ICartItemRepository _cartItemRepository;

    public GetUserCartHandler(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }


    public async Task<IEnumerable<CartItem>> Handle(GetUserCartQuery request, CancellationToken cancellationToken)
    {
        var cartItems = await _cartItemRepository.GetAllCartItemsAsync(true);
        return cartItems.Where(c => c.UserId == request.UserId).Paginate(request.PaginationParams);
    }
}