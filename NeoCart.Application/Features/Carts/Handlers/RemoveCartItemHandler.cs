using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Carts.Commands;

namespace NeoCart.Application.Features.Carts.Handlers;

public class RemoveCartItemHandler : IRequestHandler<RemoveCartItemCommand>
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveCartItemHandler(ICartItemRepository cartItemRepository, IUnitOfWork unitOfWork)
    {
        _cartItemRepository = cartItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
    {
        await _cartItemRepository.RemoveCartItemAsync(request.Id);
        await _unitOfWork.CommitChangesAsync();
    }
}