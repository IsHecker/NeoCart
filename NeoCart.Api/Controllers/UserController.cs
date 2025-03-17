using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeoCart.Application.Common;
using NeoCart.Application.Features.Users.Queries;

namespace NeoCart.Api.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly ISender _mediator;

    public UserController(ISender mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = Roles.User)]
    [HttpGet(ApiEndpoints.Users.GetUserProfile)]
    public async Task<IActionResult> GetUserProfile()
    {
        var userProfileDto = await _mediator.Send(new GetUserProfileQuery(User.GetUserId()));

        if (userProfileDto is null)
            return NotFound("User not found");

        return Ok(userProfileDto);
    }

    [Authorize(Roles = Roles.Seller)]
    [HttpGet(ApiEndpoints.Users.GetSellerDashboard)]
    public async Task<IActionResult> GetSellerDashboard()
    {
        var userProfileDto = await _mediator.Send(new GetSellerDashboardQuery(User.GetUserId()));
        return Ok(userProfileDto);
    }
}