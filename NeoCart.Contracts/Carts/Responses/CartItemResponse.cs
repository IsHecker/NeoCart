using NeoCart.Contracts.Products.Responses;

namespace NeoCart.Contracts.Carts.Responses;

public class CartItemResponse
{
    public required Guid Id { get; set; }
    public required ProductResponse Product { get; set; }
    public required int Quantity { get; set; }
}