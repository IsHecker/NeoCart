using NeoCart.Domain.Entities;

namespace NeoCart.Application.Abstractions;

public interface IReviewRepository
{
    Task<IQueryable<Review>> GetAllReviewsAsync(bool includeProduct = false);
    Task<Review> UpdateReviewAsync(Review review);
    Task DeleteReviewAsync(Guid id);
}