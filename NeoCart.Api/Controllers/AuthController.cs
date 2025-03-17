using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeoCart.Api.Mapping;
using NeoCart.Application.Common;
using NeoCart.Application.DTOs;
using NeoCart.Application.Features.Auth.Commands;
using NeoCart.Application.Results;
using NeoCart.Contracts.Authentications;
using NeoCart.Domain;

namespace NeoCart.Api.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly ISender _mediator;

    public AuthController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(ApiEndpoints.Auth.Register)]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if(!Roles.IsValidRole(request.Role))
            return BadRequest("Invalid role");
        
        var registerResult = await _mediator.Send(new RegisterCommand(request.ToRegisterDto()));

        if (!registerResult.Succeeded)
            return BadRequest(registerResult.Errors);

        var confirmationLink = Url.ActionLink(
            protocol: "https",
            action: "ConfirmEmail",
            values: new { userId = registerResult.UserId, confirmationToken = registerResult.ConfirmationToken });

        return Ok($"To confirm your email address click this link: {confirmationLink}");
    }

    [HttpPost(ApiEndpoints.Auth.Login)]
    public async Task<IActionResult> Login(SignInRequest request)
    {
        var signInResult = await _mediator.Send(new SignInCommand(request.ToSignInDto()));

        if (signInResult.IsNotAllowed)
            return Unauthorized("Account is not confirmed");

        if (!signInResult.Succeeded)
            return BadRequest(signInResult.Errors);

        return Ok("Your are now signed in");
    }

    [HttpGet(ApiEndpoints.Auth.ConfirmEmail)]
    public async Task<IActionResult> ConfirmEmail(string userId, string confirmationToken)
    {
        var confirmationResult = await _mediator.Send(new ConfirmEmailCommand(userId, confirmationToken));

        if (!confirmationResult.Succeeded)
            return BadRequest(confirmationResult.Errors);

        return Ok("Account is confirmed Successfully");
    }

    [HttpGet(ApiEndpoints.Auth.Logout)]
    public async Task<IActionResult> Logout()
    {
        await _mediator.Send(new SignOutCommand());
        return NoContent();
    }
}