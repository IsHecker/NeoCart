namespace NeoCart.Contracts.Carts.Requests;

public class AddCartItemRequest
{
    public required Guid ProductId { get; set; }
    public required int Quantity { get; set; }
}