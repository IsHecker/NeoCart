using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Carts.Commands;

namespace NeoCart.Application.Features.Carts.Handlers;

public class UpdateCartItemQuantityHandler : IRequestHandler<UpdateCartItemQuantityCommand>
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCartItemQuantityHandler(ICartItemRepository cartItemRepository, IUnitOfWork unitOfWork)
    {
        _cartItemRepository = cartItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
    {
        await _cartItemRepository.UpdateCartItemQuantityAsync(request.Id, request.Quantity);
        await _unitOfWork.CommitChangesAsync();
    }
}