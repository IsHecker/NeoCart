namespace NeoCart.Application.Common;

public class Pagination
{
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 5;
    public int PageNumber { get; set; } = DefaultPageNumber;
    public int PageSize { get; set; } = DefaultPageSize;
}