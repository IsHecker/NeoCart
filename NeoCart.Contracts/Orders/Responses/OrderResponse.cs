namespace NeoCart.Contracts.Orders.Responses;

public class OrderResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; }
    public IEnumerable<OrderItemReponse> Items { get; set; }
}