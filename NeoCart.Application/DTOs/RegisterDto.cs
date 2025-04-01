using NeoCart.Application.Common;

namespace NeoCart.Application.DTOs;

public class RegisterDto
{
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string Role { get; init; }
}