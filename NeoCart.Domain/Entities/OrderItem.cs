using NeoCart.Domain.Common;

namespace NeoCart.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; } // UNIQUEIDENTIFIER (Foreign Key → Orders) Links to the Order
    public Guid ProductId { get; set; } // UNIQUEIDENTIFIER (Foreign Key → Products) The product being purchased
    public Guid SellerId { get; set; } //  The seller of this product
    public int Quantity { get; set; } // INT (Number of items) How many units were ordered
    public decimal Price { get; set; } // DECIMAL(18,2) (Price per unit) Price per unit
    public decimal TotalPrice => Quantity * Price; // Computed total price

    //public Order Order { get; init; } = null!;
    public Product Product { get; init; } = null!;
}