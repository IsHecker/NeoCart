namespace NeoCart.Application.DTOs;

public class SignInDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public bool RememberMe { get; set; } = false;
}