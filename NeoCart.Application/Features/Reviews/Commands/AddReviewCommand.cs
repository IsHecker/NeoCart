using MediatR;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Reviews.Commands;

public record AddReviewCommand(Review Review) : IRequest<Review>;