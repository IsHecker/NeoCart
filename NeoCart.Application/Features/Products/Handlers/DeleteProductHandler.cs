using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Products.Commands;

namespace NeoCart.Application.Features.Products.Handlers;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteProductAsync(request.Id);
        await _unitOfWork.CommitChangesAsync();
    }
}