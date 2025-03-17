namespace NeoCart.Application.Common;

public class GetAllProductsOptions
{
    public string? Name { get; set; }
    public int? Year { get; set; }
    public Guid? SellerId { get; set; }
    public string? SortField { get; set; }
    public SortOrder? SortOrder { get; set; }
    public required int PageNumber { get; set; }
    public required int PageSize { get; set; }
}

public enum SortOrder
{
    Unsorted,
    Ascending,
    Descending
}