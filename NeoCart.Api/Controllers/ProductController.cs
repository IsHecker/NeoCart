using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeoCart.Api.Mapping;
using NeoCart.Application.Common;
using NeoCart.Application.Features.Product.Queries;
using NeoCart.Application.Features.Products.Commands;
using NeoCart.Application.Features.Products.Queries;
using NeoCart.Contracts.Common;
using NeoCart.Contracts.Products;
using NeoCart.Contracts.Products.Requests;

namespace NeoCart.Api.Controllers;

[Authorize(Roles = $"{Roles.Seller}, {Roles.Admin}")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ISender _mediator;

    public ProductController(ISender mediator)
    {
        _mediator = mediator;
    }


    [AllowAnonymous]
    [HttpGet(ApiEndpoints.Products.GetAll)]
    public async Task<IActionResult> GetAllProducts(
        [FromQuery] GetAllProductsRequest request,
        [FromQuery] PaginationRequest paginationRequest)
    {
        var products = await _mediator.Send(new GetAllProductsQuery(request.MapToOptions(paginationRequest)));
        return Ok(products.ToResponse(paginationRequest));
    }

    [AllowAnonymous]
    [HttpGet(ApiEndpoints.Products.GetById)]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await _mediator.Send(new GetProductbyIDQuery(id));
        return Ok(product.ToResponse());
    }

    [HttpPost(ApiEndpoints.Products.Create)]
    public async Task<IActionResult> AddProduct(CreateProductRequest request)
    {
        var product = await _mediator.Send(new CreateProductCommand(request.ToProduct(User.GetUserId())));
        return CreatedAtAction(nameof(GetProductById), new { product.Id }, product.ToResponse());
    }

    [HttpPut(ApiEndpoints.Products.Update)]
    public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductRequest request)
    {
        var product = await _mediator.Send(new UpdateProductCommand(request.ToProduct(id)));
        return Ok(product.ToResponse());
    }

    [HttpDelete(ApiEndpoints.Products.Delete)]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }
}