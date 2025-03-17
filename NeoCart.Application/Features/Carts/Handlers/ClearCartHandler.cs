using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Carts.Commands;

namespace NeoCart.Application.Features.Carts.Handlers;

public class ClearCartHandler : IRequestHandler<ClearCartCommand>
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ClearCartHandler(ICartItemRepository cartItemRepository, IUnitOfWork unitOfWork)
    {
        _cartItemRepository = cartItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ClearCartCommand request, CancellationToken cancellationToken)
    {
        await _cartItemRepository.ClearCartAsync(
            (await _cartItemRepository.GetAllCartItemsAsync())
            .Where(cartItem => cartItem.UserId == request.UserId));
        
        await _unitOfWork.CommitChangesAsync();
    }
}