using NeoCart.Application.Common;

namespace NeoCart.Application.Results;

public class RegisterResult
{
    public bool Succeeded { get; init; }
    public IEnumerable<Error>? Errors { get; init; }
    public Guid UserId { get; init; }
    public string? ConfirmationToken { get; init; }
}