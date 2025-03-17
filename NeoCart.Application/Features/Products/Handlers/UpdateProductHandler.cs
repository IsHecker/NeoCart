using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Products.Commands;

namespace NeoCart.Application.Features.Products.Handlers;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Domain.Entities.Product>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Domain.Entities.Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.UpdateProductAsync(request.Product);
        await _unitOfWork.CommitChangesAsync();
        return product;
    }
}