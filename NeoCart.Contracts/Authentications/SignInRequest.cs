namespace NeoCart.Contracts.Authentications;

public class SignInRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public bool RememberMe { get; set; }
}