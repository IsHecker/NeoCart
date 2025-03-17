using Microsoft.AspNetCore.Identity;
using NeoCart.Application.Abstractions;
using NeoCart.Application.Common;
using NeoCart.Application.DTOs;
using NeoCart.Application.Results;

namespace NeoCart.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    private readonly SignInManager<IdentityUser<Guid>> _signInManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;


    public AuthService(UserManager<IdentityUser<Guid>> userManager, SignInManager<IdentityUser<Guid>> signInManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task<RegisterResult> RegisterAsync(RegisterDto registerDto)
    {
        var user = new IdentityUser<Guid>
        {
            Email = registerDto.Email,
            UserName = registerDto.Username,
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        
        if (!result.Succeeded)
            return new RegisterResult
            {
                Succeeded = result.Succeeded,
                Errors = result.Errors.Select(e => new Error(e.Code, e.Description))
            };

        await _userManager.AddToRoleAsync(user, registerDto.Role);

        var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        return new RegisterResult
        {
            UserId = user.Id,
            ConfirmationToken = confirmationToken,
            Succeeded = result.Succeeded,
            Errors = result.Errors.Select(e => new Error(e.Code, e.Description))
        };
    }

    public async Task<Application.Results.SignInResult> SignInAsync(SignInDto signInDto)
    {
        var user = await _userManager.FindByEmailAsync(signInDto.Email);

        if (user == null)
            return new Application.Results.SignInResult
            {
                Succeeded = false,
                Errors = [new Error("Email", "Invalid Email")]
            };

        var result = await _signInManager.PasswordSignInAsync(
            user,
            signInDto.Password,
            isPersistent: false,
            lockoutOnFailure: true);

        var httpContext = _signInManager.Context.User;

        return new Application.Results.SignInResult
        {
            Succeeded = result.Succeeded,
            IsLockedOut = result.IsLockedOut,
            IsNotAllowed = result.IsNotAllowed,
            RequiresTwoFactor = result.RequiresTwoFactor,
        };
    }

    public async Task<ConfirmationResult> ConfirmEmailAsync(string userId, string confirmationToken)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return new ConfirmationResult
            {
                Succeeded = false,
                Errors = [new Error("Not Found", "User Not Found")]
            };

        var result = await _userManager.ConfirmEmailAsync(user, confirmationToken);

        return new ConfirmationResult
        {
            Succeeded = result.Succeeded,
            Errors = result.Errors.Select(e => new Error(e.Code, e.Description))
        };
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}