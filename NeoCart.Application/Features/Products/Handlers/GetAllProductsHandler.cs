using System.Linq.Expressions;
using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Common;
using NeoCart.Application.Features.Product.Queries;

namespace NeoCart.Application.Features.Products.Handlers;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Domain.Entities.Product>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Domain.Entities.Product>> Handle(GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        var options = request.Options;
        var products = await _productRepository.GetAllProductsAsync();
        // return products.Skip(request.PageSize * (request.PageNumber - 1)).Take(request.PageSize);

        if (options.SellerId.HasValue)
            products = products.Where(p => p.SellerId == options.SellerId.Value);

        if (options.Name is not null)
            products = products.Where(p => p.Name.Contains(options.Name));

        if (!string.IsNullOrEmpty(options.SortField))
        {
            var param = Expression.Parameter(typeof(Domain.Entities.Product), "p");
            var property = Expression.Property(param, options.SortField); // Access property dynamically

            // Correctly define lambda: Expression<Func<Product, TKey>>
            var delegateType = typeof(Func<,>).MakeGenericType(typeof(Domain.Entities.Product), property.Type);
            var lambda = Expression.Lambda(delegateType, property, param);

            products = options.SortOrder == SortOrder.Ascending
                ? Queryable.OrderBy(products, (dynamic)lambda)
                : Queryable.OrderByDescending(products, (dynamic)lambda);

            // // Get OrderBy or OrderByDescending dynamically
            // var methodName = options.SortOrder == SortOrder.Ascending ? "OrderBy" : "OrderByDescending";
            // var sortingMethod = typeof(Queryable)
            //     .GetMethods()
            //     .First(m => m.Name == methodName && m.GetParameters().Length == 2)
            //     .MakeGenericMethod(typeof(Domain.Entities.Product), property.Type);
            //
            // // Apply sorting to the IQueryable<T>
            // products = (IQueryable<Domain.Entities.Product>)sortingMethod.Invoke(null, [products, lambda])!;
        }

        return products.Skip(options.PageSize * (options.PageNumber - 1)).Take(options.PageSize);
    }
}