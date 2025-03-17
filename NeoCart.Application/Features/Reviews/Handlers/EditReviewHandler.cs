using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Reviews.Commands;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Reviews.Handlers;

public class EditReviewHandler : IRequestHandler<EditReviewCommand, Review>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditReviewHandler(IReviewRepository reviewRepository, IUnitOfWork unitOfWork)
    {
        _reviewRepository = reviewRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Review> Handle(EditReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _reviewRepository.UpdateReviewAsync(request.Review);
        await _unitOfWork.CommitChangesAsync();
        return review;
    }
}