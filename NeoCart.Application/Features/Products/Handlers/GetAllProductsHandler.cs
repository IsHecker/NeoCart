using System.Linq.Expressions;
using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Common;
using NeoCart.Application.DTOs;
using NeoCart.Application.DTOs.FilterOptions;
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
        var options = request.Filter;
        var pagination = request.PaginationParams;

        var products = await _productRepository.GetAllProductsAsync(options.IncludeReviews);

        if (options.SellerId.HasValue)
            products = products.Where(p => p.SellerId == options.SellerId.Value);

        if (options.Name is not null)
            products = products.Where(p => p.Name.Contains(options.Name));

        if (!string.IsNullOrEmpty(options.SortField))
        {
            var param = Expression.Parameter(typeof(Domain.Entities.Product), "p");
            var property = Expression.Property(param, options.SortField); // Access property dynamically

            var delegateType = typeof(Func<,>).MakeGenericType(typeof(Domain.Entities.Product), property.Type);
            var lambda = Expression.Lambda(delegateType, property, param);

            products = options.SortOrder == SortOrder.Ascending
                ? Queryable.OrderBy(products, (dynamic)lambda)
                : Queryable.OrderByDescending(products, (dynamic)lambda);
        }

        return products.Paginate(pagination);
    }
}