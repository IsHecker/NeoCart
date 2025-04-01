using Microsoft.EntityFrameworkCore;
using NeoCart.Application.Abstractions;
using NeoCart.Domain.Entities;

namespace NeoCart.Infrastructure.Persistence.Repositories;

public class CartItemRepository : ICartItemRepository
{
    private readonly AppDbContext _context;

    public CartItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<IQueryable<CartItem>> GetAllCartItemsAsync(bool includeProduct = false)
    {
        IQueryable<CartItem> cartItems = _context.CartItems;

        if (includeProduct)
            cartItems = cartItems.Include(c => c.Product);

        return Task.FromResult(cartItems);
    }

    public async Task<CartItem?> AddCartItemAsync(CartItem cartItem)
    {
        if (await _context.Products.FindAsync(cartItem.ProductId) is null)
            return null;

        await _context.CartItems.AddAsync(cartItem);
        return cartItem;
    }

    public async Task UpdateCartItemQuantityAsync(Guid id, int quantity)
    {
        var cartItem = await _context.CartItems.FindAsync(id);

        if (cartItem is null)
            throw new KeyNotFoundException();

        _context.Entry(cartItem).CurrentValues.SetValues(new { Quantity = quantity });
    }

    public Task RemoveCartItemAsync(Guid id)
    {
        _context.CartItems.Remove(new CartItem { Id = id, UserId = Guid.Empty, ProductId = Guid.Empty, Quantity = 0 });
        return Task.CompletedTask;
    }

    public async Task<int> ClearCartAsync(Guid userId)
    {
        return await _context.CartItems.Where(cartItem => cartItem.UserId == userId).ExecuteDeleteAsync();
    }
}