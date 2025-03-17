namespace NeoCart.Contracts.Common;

public class PaginationRequest
{
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 5;
    public int PageNumber { get; set; } = DefaultPageNumber;
    public int PageSize { get; set; } = DefaultPageSize;
}