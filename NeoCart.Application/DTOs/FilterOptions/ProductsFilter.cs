namespace NeoCart.Application.DTOs.FilterOptions;

public class ProductsFilter
{
    public string? Name { get; set; }
    public int? Year { get; set; }
    public Guid? SellerId { get; set; }
    public string? SortField { get; set; }
    public SortOrder? SortOrder { get; set; }
    public bool IncludeReviews { get; set; }
}

public enum SortOrder
{
    Unsorted,
    Ascending,
    Descending
}