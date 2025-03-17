using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Reviews.Commands;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Reviews.Handlers;

public class AddReviewHandler : IRequestHandler<AddReviewCommand, Review>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddReviewHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Review> Handle(AddReviewCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(request.Review.ProductId);
        if (product == null)
            throw new KeyNotFoundException("Product not found.");

        product.AddReview(request.Review);

        await _productRepository.UpdateProductAsync(product);
        
        await _unitOfWork.CommitChangesAsync();
        return request.Review;
    }
}