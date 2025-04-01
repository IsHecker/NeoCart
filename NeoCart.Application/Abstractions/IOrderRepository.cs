using NeoCart.Domain.Entities;

namespace NeoCart.Application.Abstractions;

public interface IOrderRepository
{
    Task<IQueryable<Order>> GetAllOrdersAsync(bool includeOrderItem = false);
    Task<IQueryable<OrderItem>> GetAllOrderItemsAsync();
    Task<Order?> GetOrderByIdAsync(Guid id);
    Task<IQueryable<Order>?> GetOrderByUserIdAsync(Guid userId, bool includeOrderItems = false);
    Task AddOrderAsync(Order order);
    Task<int> RemoveOrderAsync(Guid id);
}