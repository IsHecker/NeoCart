using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeoCart.Api.Mapping;
using NeoCart.Application.Common;
using NeoCart.Application.DTOs;
using NeoCart.Application.Features.Carts.Commands;
using NeoCart.Application.Features.Carts.Queries;
using NeoCart.Contracts.Carts.Requests;
using NeoCart.Contracts.Common;
using NeoCart.Contracts.Products.Requests;

namespace NeoCart.Api.Controllers;

[Authorize(Roles = Roles.User)]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ISender _mediator;

    public CartController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(ApiEndpoints.Carts.GetUserCart)]
    public async Task<IActionResult> GetUserCart([FromQuery] PaginationParams paginationParams)
    {
        var cartItems = await _mediator.Send(new GetUserCartQuery(User.GetUserId(), paginationParams));
        return Ok(cartItems.ToResponse(paginationParams));
    }
    
    [HttpPost(ApiEndpoints.Carts.AddItem)]
    public async Task<IActionResult> AddCartItem(AddCartItemRequest request)
    {
        var cartItem = await _mediator.Send(new AddCartItemCommand(request.ToCartItem(User.GetUserId())));
        
        if(cartItem is null)
            return NotFound("Product not found");
        
        return Ok("Item is Added Successfully!");
    }
    
    [HttpPut(ApiEndpoints.Carts.UpdateItemQuantity)]
    public async Task<IActionResult> UpdateCartItemQuantity(Guid itemId, UpdateCartItemQuantityRequest request)
    {
        await _mediator.Send(new UpdateCartItemQuantityCommand(itemId, request.Quantity));
        return Ok("Item is Updated Successfully!");
    }
    
    [HttpDelete(ApiEndpoints.Carts.RemoveItem)]
    public async Task<IActionResult> RemoveCartItem(Guid itemId)
    {
        await _mediator.Send(new RemoveCartItemCommand(itemId));
        return NoContent();
    }
    
    [HttpDelete(ApiEndpoints.Carts.Clear)]
    public async Task<IActionResult> ClearCart()
    {
        await _mediator.Send(new ClearCartCommand(User.GetUserId()));
        return NoContent();
    }
}