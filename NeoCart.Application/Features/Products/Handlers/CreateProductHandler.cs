using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Products.Commands;

namespace NeoCart.Application.Features.Products.Handlers;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Domain.Entities.Product>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Domain.Entities.Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.AddProductAsync(request.Product);
        await _unitOfWork.CommitChangesAsync();
        return product;
    }
}