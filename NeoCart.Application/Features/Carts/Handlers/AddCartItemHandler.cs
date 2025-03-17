using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Carts.Commands;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Carts.Handlers;

public class AddCartItemHandler : IRequestHandler<AddCartItemCommand, CartItem?>
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddCartItemHandler(ICartItemRepository cartItemRepository, IUnitOfWork unitOfWork)
    {
        _cartItemRepository = cartItemRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<CartItem?> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await _cartItemRepository.AddCartItemAsync(request.CartItem);
        await _unitOfWork.CommitChangesAsync();
        return cartItem;
    }
}