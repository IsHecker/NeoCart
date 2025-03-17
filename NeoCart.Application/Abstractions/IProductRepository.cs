using NeoCart.Application.Common;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Abstractions;

public interface IProductRepository
{
    // Task<IEnumerable<Product>> GetAllProductsAsync(GetAllProductsOptions options);
    Task<IQueryable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<Product> AddProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task DeleteProductAsync(Guid id);
}