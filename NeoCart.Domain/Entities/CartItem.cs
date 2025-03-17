using NeoCart.Domain.Common;

namespace NeoCart.Domain.Entities;

public class CartItem : BaseEntity
{
    public required Guid UserId { get; set; }
    public required Guid ProductId { get; set; }
    public required int Quantity { get; set; }
    
    public Product? Product { get; init; }
}