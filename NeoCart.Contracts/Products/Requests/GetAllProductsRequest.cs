namespace NeoCart.Contracts.Products.Requests;

public class GetAllProductsRequest
{
    public string? Name { get; set; }
    public string? SortBy { get; set; }
    public Guid? SellerId { get; set; }
    public int? Year { get; set; }
}