using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Common;
using NeoCart.Domain.Entities;

namespace NeoCart.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<IQueryable<Product>> GetAllProductsAsync(bool includeReviews = false)
    {
        IQueryable<Product> products = _context.Products;

        if (includeReviews)
            products = products.Include(p => p.Reviews);

        return Task.FromResult(products);
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await _context.Products
            .Include(p => p.Reviews)
            .FirstOrDefaultAsync(product => product.Id == id);
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        return product;
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        var productToUpdate = await _context.Products.AsTracking()
            .FirstOrDefaultAsync(p => p.Id == product.Id);

        if (productToUpdate is null)
            throw new KeyNotFoundException();

        productToUpdate.Name = product.Name.IsNullOrEmpty() ? productToUpdate.Name : product.Name;
        productToUpdate.Description = product.Description ?? productToUpdate.Description;
        productToUpdate.Price = product.Price < 1 ? productToUpdate.Price : product.Price;
        productToUpdate.DateUpdated = DateTime.Now;

        return productToUpdate;
    }

    public async Task<int> DeleteProductAsync(Guid id)
    {
        return await _context.Products.Where(p => p.Id == id).ExecuteDeleteAsync();
    }
}