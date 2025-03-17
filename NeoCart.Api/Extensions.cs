using System.Security.Claims;

namespace NeoCart.Api;

public static class Extensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        return Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
    }
}