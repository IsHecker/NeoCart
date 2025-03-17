using NeoCart.Application.DTOs;

namespace NeoCart.Application.Abstractions;

public interface IUserService
{
    Task<UserProfileDto?> GetUserProfile(Guid userId);
}