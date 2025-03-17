using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Orders.Queries;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Orders.Handlers;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Order>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.Id);
        if (order is null)
            throw new KeyNotFoundException("Order not found");
        return order;
    }
}