using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Features.Reviews.Commands;

namespace NeoCart.Application.Features.Reviews.Handlers;

public class DeleteReviewHandler : IRequestHandler<DeleteReviewCommand>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteReviewHandler(IReviewRepository reviewRepository, IUnitOfWork unitOfWork)
    {
        _reviewRepository = reviewRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        await _reviewRepository.DeleteReviewAsync(request.Id);
        await _unitOfWork.CommitChangesAsync();
    }
}