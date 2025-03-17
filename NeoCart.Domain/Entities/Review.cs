using NeoCart.Domain.Common;

namespace NeoCart.Domain.Entities;

public class Review : BaseEntity
{
    public required Guid UserId { get; set; }
    public required Guid ProductId { get; set; }
    public required int Rating { get; set; }     // 1-5 star rating
    public string? Comment { get; set; }
    
    public Product? Product { get; init; }
}