using NeoCart.Application.DTOs;

namespace NeoCart.Contracts.Products.Requests;

public class ProductsFilterRequest
{
    public string? Name { get; set; }
    public string? SortBy { get; set; }
    public Guid? SellerId { get; set; }
    public int? Year { get; set; }
    public bool Reviews { get; set; } = false;
}