using NeoCart.Domain.Entities;

namespace NeoCart.Application.Abstractions;

public interface ICartItemRepository
{
    Task<IQueryable<CartItem>> GetAllCartItemsAsync(bool includeProduct = false);
    Task<CartItem?> AddCartItemAsync(CartItem cartItem);
    Task UpdateCartItemQuantityAsync(Guid id, int quantity);
    Task RemoveCartItemAsync(Guid id);
    Task<int> ClearCartAsync(Guid userId);
}