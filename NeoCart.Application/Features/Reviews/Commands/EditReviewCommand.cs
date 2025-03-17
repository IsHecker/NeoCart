using MediatR;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Reviews.Commands;

public record EditReviewCommand(Review Review) : IRequest<Review>;