using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Product.Queries;
using NeoCart.Application.Features.Products.Queries;

namespace NeoCart.Application.Features.Products.Handlers;

public class GetProductByIdHandler : IRequestHandler<GetProductbyIdQuery, Domain.Entities.Product>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Domain.Entities.Product> Handle(GetProductbyIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(request.Id);
        if (product == null)
            throw new KeyNotFoundException("Product not found.");
        return product;
    }
}