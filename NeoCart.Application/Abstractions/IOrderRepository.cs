using NeoCart.Domain.Entities;

namespace NeoCart.Application.Abstractions;

public interface IOrderRepository
{
    Task<IQueryable<Order>> GetAllOrdersAsync(bool includeOrderItem = false);
    Task<IQueryable<OrderItem>> GetAllOrderItemsAsync();
    Task<Order?> GetOrderByIdAsync(Guid id);
    Task<IEnumerable<Order>?> GetOrderByUserIdAsync(Guid userId);
    Task AddOrderAsync(Order order);
    Task RemoveOrderAsync(Order order);
}