using Microsoft.AspNetCore.Identity;
using NeoCart.Application.Abstractions;
using NeoCart.Application.DTOs;

namespace NeoCart.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUser<Guid>> _userManager;

    public UserService(UserManager<IdentityUser<Guid>> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserProfileDto?> GetUserProfile(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user is null)
            return null;
        
        return new UserProfileDto
        {
            Email = user.Email!,
            Username = user.UserName!,
            Roles = await _userManager.GetRolesAsync(user)
        };
    }
}