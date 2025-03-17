namespace NeoCart.Contracts.Products.Requests;

public class CreateProductRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required decimal Price { get; set; }
}