using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeoCart.Api.Mapping;
using NeoCart.Application.Common;
using NeoCart.Application.DTOs;
using NeoCart.Application.Features.Orders.Commands;
using NeoCart.Application.Features.Orders.Queries;
using NeoCart.Contracts.Common;
using NeoCart.Contracts.Products.Requests;

namespace NeoCart.Api.Controllers;

[Authorize(Roles = Roles.User)]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ISender _mediator;

    public OrderController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(ApiEndpoints.Orders.GetById)]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var order = await _mediator.Send(new GetOrderByIdQuery(id));
        return Ok(order.ToResponse());
    }

    [HttpGet(ApiEndpoints.Orders.GetUserOrders)]
    public async Task<IActionResult> GetUserOrders(
        [FromQuery] PaginationParams paginationParams,
        [FromQuery] bool orderItems = true)
    {
        var order = await _mediator.Send(new GetUserOrdersQuery(User.GetUserId(), orderItems, paginationParams));
        return Ok(order.ToResponse(paginationParams));
    }

    [HttpPost(ApiEndpoints.Orders.Checkout)]
    public async Task<IActionResult> Checkout()
    {
        var order = await _mediator.Send(new CheckoutCommand(User.GetUserId()));
        return Ok(order.ToResponse());
    }

    [HttpDelete(ApiEndpoints.Orders.CancelUserOrder)]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        await _mediator.Send(new CancelOrderCommand(id));
        return NoContent();
    }
}