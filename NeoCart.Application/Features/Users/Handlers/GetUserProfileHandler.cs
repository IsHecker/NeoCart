using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.DTOs;
using NeoCart.Application.Features.Users.Queries;

namespace NeoCart.Application.Features.Users.Handlers;

public class GetUserProfileHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto?>
{
    private readonly IUserService _userService;

    public GetUserProfileHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserProfileDto?> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetUserProfile(request.UserId);
    }
}