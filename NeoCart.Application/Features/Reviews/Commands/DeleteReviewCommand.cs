using MediatR;

namespace NeoCart.Application.Features.Reviews.Commands;

public record DeleteReviewCommand(Guid Id) : IRequest;