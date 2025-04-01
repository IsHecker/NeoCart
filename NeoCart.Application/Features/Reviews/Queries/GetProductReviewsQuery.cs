using MediatR;
using NeoCart.Application.DTOs;
using NeoCart.Domain.Entities;

namespace NeoCart.Application.Features.Reviews.Queries;

public record GetProductReviewsQuery(Guid ProductId, PaginationParams PaginationParams) : IRequest<IEnumerable<Review>>;