namespace NeoCart.Contracts.Reviews.Responses;

public class ReviewResponse
{
    public required Guid Id { get; set; }
    public required Guid UserId { get; set; } 
    public required Guid ProductId { get; set; }
    public required int Rating { get; set; }     // 1-5 star rating
    public string? Comment { get; set; }
    public DateTime DateCreated { get; set; }
}