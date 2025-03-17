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

    // public Task<IEnumerable<Product>> GetAllProductsAsync(GetAllProductsOptions options)
    // {
    //     // return Task.FromResult<IEnumerable<Product>>(_context.Products
    //     //     .Include(p => p.Reviews)
    //     //     .Skip(options.PageSize!.Value * (options.PageNumber!.Value - 1))
    //     //     .Take(options.PageSize.Value));
    // }
    
    public Task<IQueryable<Product>> GetAllProductsAsync()
    {
        return Task.FromResult<IQueryable<Product>>(_context.Products
            .Include(p => p.Reviews));
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
        var productToUpdate = await _context.Products.FindAsync(product.Id);

        if (productToUpdate is null)
            throw new KeyNotFoundException();

        // _context.Entry(productToUpdate).CurrentValues.SetValues(new
        // {
        //     product.Name,
        //     product.Description,
        //     product.Price,
        // });

        productToUpdate.Name = product.Name.IsNullOrEmpty() ? productToUpdate.Name : product.Name;
        productToUpdate.Description = product.Description ?? productToUpdate.Description;
        productToUpdate.Price = product.Price < 1 ? productToUpdate.Price : product.Price;
        productToUpdate.DateUpdated = DateTime.Now;

        return productToUpdate;
    }

    public Task DeleteProductAsync(Guid id)
    {
        _context.Products.Remove(new Product { Id = id, Name = null!, Price = 0, SellerId = Guid.Empty });
        return Task.CompletedTask;
    }
}