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
        var order = await _orderRepository.GetOrderByIdAsync(request.Id);
        if (order is null)
            throw new KeyNotFoundException("Order not found");
        
        if (order.Status != "Pending")
            return;
        
        await _orderRepository.RemoveOrderAsync(order);
        await _unitOfWork.CommitChangesAsync();
    }
}