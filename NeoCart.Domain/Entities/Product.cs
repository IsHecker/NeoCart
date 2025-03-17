using NeoCart.Domain.Common;

namespace NeoCart.Domain.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Rating { get; set; }
    public int TotalRatings { get; set; }
    public int TotalSold { get; set; }
    public required decimal Price { get; set; }
    public required Guid SellerId { get; set; }


    public ICollection<CartItem>? CartItems { get; private set; }
    //public IReadOnlyCollection<Review>? Reviews { get; private set; }

    private readonly List<Review> _reviews = [];
    public IReadOnlyCollection<Review> Reviews => _reviews;

    public void AddReview(Review newReview)
    {
        ArgumentNullException.ThrowIfNull(newReview);

        _reviews.Add(newReview);

        TotalRatings++;

        Rating = decimal.Round(Reviews.Sum(r => r.Rating) / (decimal)TotalRatings, 1);
    }
}