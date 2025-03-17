namespace NeoCart.Contracts.Reviews.Requests;

public class EditReviewRequest
{
    public int? Rating { get; set; }
    public string? Comment { get; set; }
}