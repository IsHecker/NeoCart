namespace NeoCart.Contracts;

public class BaseResponse
{
    public required Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
}