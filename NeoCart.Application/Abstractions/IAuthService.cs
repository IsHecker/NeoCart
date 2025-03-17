using NeoCart.Application.DTOs;
using NeoCart.Application.Results;

namespace NeoCart.Application.Abstractions;

public interface IAuthService
{
    Task<RegisterResult> RegisterAsync(RegisterDto registerDto);
    Task<SignInResult> SignInAsync(SignInDto signInDto);
    Task<ConfirmationResult> ConfirmEmailAsync(string userId, string confirmationToken);
    Task SignOutAsync();
}