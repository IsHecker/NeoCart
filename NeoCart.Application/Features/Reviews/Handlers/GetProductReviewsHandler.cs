using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Reviews.Queries;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Reviews.Handlers;

public class GetProductReviewsHandler : IRequestHandler<GetProductReviewsQuery, IEnumerable<Review>>
{
    private readonly IReviewRepository _reviewRepository;

    public GetProductReviewsHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<IEnumerable<Review>> Handle(GetProductReviewsQuery request, CancellationToken cancellationToken)
    {
        return (await _reviewRepository.GetAllReviewsAsync()).Where(review => review.ProductId == request.ProductId);
    }
}