using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeoCart.Api.Mapping;
using NeoCart.Application.Common;
using NeoCart.Application.Features.Reviews.Commands;
using NeoCart.Application.Features.Reviews.Queries;
using NeoCart.Contracts.Common;
using NeoCart.Contracts.Products.Requests;
using NeoCart.Contracts.Reviews.Requests;

namespace NeoCart.Api.Controllers;

[Authorize(Roles = Roles.User)]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly ISender _mediator;

    public ReviewController(ISender mediator)
    {
        _mediator = mediator;
    }


    [AllowAnonymous]
    [HttpGet(ApiEndpoints.Reviews.GetByProduct)]
    public async Task<IActionResult> GetProductReviews(Guid productId, [FromQuery] PaginationRequest paginationRequest)
    {
        var reviews = await _mediator.Send(new GetProductReviewsQuery(productId));
        return Ok(reviews.ToResponse(paginationRequest));
    }

    [HttpPost(ApiEndpoints.Reviews.Add)]
    public async Task<IActionResult> AddReview(Guid productId, [FromBody] AddReviewRequest request)
    {
        var reviewToAdd = request.ToReview(productId, User.GetUserId());
        var review = await _mediator.Send(new AddReviewCommand(reviewToAdd));
        return Ok(review.ToResponse());
    }

    [HttpPut(ApiEndpoints.Reviews.Update)]
    public async Task<IActionResult> EditReview(Guid id, [FromBody] EditReviewRequest request)
    {
        var review = await _mediator.Send(new EditReviewCommand(request.ToReview(id, User.GetUserId())));
        return Ok(review.ToResponse());
    }

    [HttpDelete(ApiEndpoints.Reviews.Delete)]
    public async Task<IActionResult> DeleteReview(Guid id)
    {
        await _mediator.Send(new DeleteReviewCommand(id));
        return NoContent();
    }
}