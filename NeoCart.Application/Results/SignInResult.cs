using NeoCart.Application.Common;

namespace NeoCart.Application.Results;

public class SignInResult
{
    public bool Succeeded { get; init; }
    public bool IsLockedOut { get; init; }
    public bool IsNotAllowed { get; init; }
    public bool RequiresTwoFactor { get; init; }

    public IEnumerable<Error>? Errors { get; init; }
}