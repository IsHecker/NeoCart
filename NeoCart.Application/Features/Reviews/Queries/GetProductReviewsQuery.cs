using MediatR;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Reviews.Queries;

public record GetProductReviewsQuery(Guid ProductId) : IRequest<IEnumerable<Review>>;