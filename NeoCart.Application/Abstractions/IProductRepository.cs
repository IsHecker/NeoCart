using NeoCart.Application.Common;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Abstractions;

public interface IProductRepository
{
    Task<IQueryable<Product>> GetAllProductsAsync(bool includeReviews = false);
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<Product> AddProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task<int> DeleteProductAsync(Guid id);
}