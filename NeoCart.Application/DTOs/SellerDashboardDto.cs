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
    public Guid ProductId { get; set; } // Product Identifier
    public string ProductName { get; set; } // Product Name
    public int TotalQuantitySold { get; set; } // Total units sold
    public decimal TotalRevenue { get; set; } // Total earnings from this product (Quantity * Price)
}