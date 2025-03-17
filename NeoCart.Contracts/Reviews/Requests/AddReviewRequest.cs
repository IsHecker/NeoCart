namespace NeoCart.Contracts.Reviews.Requests;

public class AddReviewRequest
{
    public required int Rating { get; set; }
    public string? Comment { get; set; }
}