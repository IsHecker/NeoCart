using MediatR;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Reviews.Commands;

public record UpdateReviewCommand(Review Review) : IRequest<Review>;