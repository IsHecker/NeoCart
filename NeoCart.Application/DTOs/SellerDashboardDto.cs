namespace NeoCart.Application.DTOs;

public class SellerDashboardDto
{
    public int TotalOrders { get; set; } // The number of unique orders that contain at least one product from the seller.
    public int TotalProductsSold { get; set; } // Total units of products sold
    public int TotalProductsListed { get; set; } // Number of products listed by the seller
    public decimal AverageOrderValue { get; set; } // Average revenue per order
    public decimal SellerRating { get; set; } // Seller’s rating from customer reviews (1-5)
    public int TotalReviews { get; set; } // Total number of reviews received
    
    public IEnumerable<TopSellingProductDto>? BestSellingProducts { get; init; } // Top-selling products
}

public class TopSellingProductDto
{
    public Guid ProductId { get; init; }
    public required string ProductName { get; init; }
    public int TotalQuantitySold { get; init; }
    public decimal TotalRevenue { get; init; } // Total earnings from this product (Quantity * Price)
}