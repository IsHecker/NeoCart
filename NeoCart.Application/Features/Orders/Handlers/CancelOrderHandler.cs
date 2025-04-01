using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Orders.Commands;

namespace NeoCart.Application.Features.Orders.Handlers;

public class CancelOrderHandler : IRequestHandler<CancelOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CancelOrderHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var deletedOrders = await _orderRepository.RemoveOrderAsync(request.Id);

        if (deletedOrders < 1)
            throw new KeyNotFoundException("Order not found");

        await _unitOfWork.CommitChangesAsync();
    }
}