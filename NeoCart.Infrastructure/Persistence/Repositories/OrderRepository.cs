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

        return Task.FromResult(orders);
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

    public Task<IQueryable<Order>?> GetOrderByUserIdAsync(Guid userId, bool includeOrderItems = false)
    {
        IQueryable<Order> orders = _context.Orders;

        if (includeOrderItems)
            orders = orders.Include(order => order.OrderItems);

        return Task.FromResult<IQueryable<Order>?>(orders.Where(order => order.UserId == userId));
    }

    public async Task AddOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public async Task<int> RemoveOrderAsync(Guid id)
    {
        return await _context.Orders.Where(order => order.Id == id).ExecuteDeleteAsync();
    }
}