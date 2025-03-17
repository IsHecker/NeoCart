using Microsoft.EntityFrameworkCore;
using NeoCart.Application.Abstractions;
using NeoCart.Domain.Entities;

namespace NeoCart.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }


    public Task<IQueryable<Order>> GetAllOrdersAsync(bool includeOrderItem = false)
    {
        IQueryable<Order> orders = _context.Orders;

        if (includeOrderItem)
            orders = orders.Include(c => c.OrderItems);

        return Task.FromResult(orders.AsQueryable());
    }

    public Task<IQueryable<OrderItem>> GetAllOrderItemsAsync()
    {
        return Task.FromResult(_context.OrderItems.AsQueryable());
    }

    public async Task<Order?> GetOrderByIdAsync(Guid id)
    {
        return await _context.Orders
            .Include(order => order.OrderItems)
            .FirstOrDefaultAsync(order => order.Id == id);
    }

    public Task<IEnumerable<Order>?> GetOrderByUserIdAsync(Guid userId)
    {
        return Task.FromResult<IEnumerable<Order>?>(_context.Orders
            .Include(order => order.OrderItems)
            .Where(order => order.UserId == userId));
    }

    public async Task AddOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public Task RemoveOrderAsync(Order order)
    {
        _context.Orders.Remove(order);
        return Task.CompletedTask;
    }
}