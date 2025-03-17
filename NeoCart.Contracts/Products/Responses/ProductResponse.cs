using System.Text.Json.Serialization;
using NeoCart.Contracts.Reviews.Responses;

namespace NeoCart.Contracts.Products.Responses;

public class ProductResponse
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Rating { get; set; }
    public int TotalRatings { get; set; }
    public required decimal Price { get; set; }
    public required Guid SellerId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<ReviewResponse>? Reviews { get; set; }

    public DateTime DateCreated { get; set; }
}