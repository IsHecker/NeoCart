using NeoCart.Application.Common;

namespace NeoCart.Application.Results;

public class ConfirmationResult
{
    public bool Succeeded { get; set; }
    public IEnumerable<Error>? Errors { get; set; } = null!;
}