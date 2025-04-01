using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Orders.Queries;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Orders.Handlers;

public class GetUserOrdersHandler : IRequestHandler<GetUserOrdersQuery, IEnumerable<Order>>
{
    private readonly IOrderRepository _orderRepository;

    public GetUserOrdersHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<Order>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
    {
        return (await _orderRepository.GetOrderByUserIdAsync(request.UserId, request.IncludeOrderItems))?
            .Paginate(request.PaginationParams) ?? Enumerable.Empty<Order>();
    }
}