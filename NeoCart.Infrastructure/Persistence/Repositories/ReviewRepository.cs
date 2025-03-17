using Microsoft.EntityFrameworkCore;
using NeoCart.Application.Abstractions;
using NeoCart.Domain.Entities;

namespace NeoCart.Infrastructure.Persistence.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext _context;

    public ReviewRepository(AppDbContext context)
    {
        _context = context;
    }


    public Task<IQueryable<Review>> GetAllReviewsAsync(bool includeProduct = false)
    {
        IQueryable<Review> reviews = _context.Reviews;

        if (includeProduct)
            reviews = reviews.Include(r => r.Product);

        return Task.FromResult(reviews.AsQueryable());

        return Task.FromResult(_context.Reviews.AsQueryable());
    }

    public async Task<Review> UpdateReviewAsync(Review review)
    {
        var reviewToUpdate = await _context.Reviews.FindAsync(review.Id);

        if (reviewToUpdate is null)
            throw new KeyNotFoundException();

        _context.Entry(reviewToUpdate).CurrentValues.SetValues(new
        {
            Rating = review.Rating > 0 ? review.Rating : reviewToUpdate.Rating,
            Comment = review.Comment ?? reviewToUpdate.Comment,
            DateUpdated = DateTime.Now
        });

        return reviewToUpdate;
    }

    public Task DeleteReviewAsync(Guid id)
    {
        _context.Reviews.Remove(new Review { Id = id, Rating = 0, ProductId = Guid.Empty, UserId = Guid.Empty });
        return Task.CompletedTask;
    }
}