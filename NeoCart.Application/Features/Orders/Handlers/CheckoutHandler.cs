using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Orders.Commands;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Orders.Handlers;

public class CheckoutHandler : IRequestHandler<CheckoutCommand, Order>
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CheckoutHandler(ICartItemRepository cartItemRepository, IUnitOfWork unitOfWork,
        IOrderRepository orderRepository)
    {
        _cartItemRepository = cartItemRepository;
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
    }

    public async Task<Order> Handle(CheckoutCommand request, CancellationToken cancellationToken)
    {
        var cartItems = (await _cartItemRepository.GetAllCartItemsAsync(true))
            .Where(cartItem => cartItem.UserId == request.UserId);

        var orderItems = cartItems.Select(cartItem => new OrderItem
        {
            ProductId = cartItem.ProductId,
            Quantity = cartItem.Quantity,
            Price = cartItem.Product!.Price,
            SellerId = cartItem.Product.SellerId,
            Product = cartItem.Product
        }).ToList();

        orderItems.ForEach(orderItem => orderItem.Product.TotalSold += orderItem.Quantity);

        var order = new Order
        {
            UserId = request.UserId,
            OrderItems = orderItems,
            TotalPrice = orderItems.Sum(o => o.TotalPrice),
            Status = "Pending"
        };

        await _orderRepository.AddOrderAsync(order);
        await _cartItemRepository.ClearCartAsync(cartItems);
        await _unitOfWork.CommitChangesAsync();
        return order;
    }
}