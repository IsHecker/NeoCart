using NeoCart.Domain.Common;

namespace NeoCart.Domain.Entities;

public class Order : BaseEntity
{
    public Guid UserId { get; set; } // UNIQUEIDENTIFIER (Foreign Key → Users)
    public decimal TotalPrice { get; set; } // DECIMAL(18,2)
    public string Status { get; set; } // NVARCHAR(50) (e.g., "Pending", "Paid")

    public ICollection<OrderItem> OrderItems { get; set; } = [];
}