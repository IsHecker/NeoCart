using NeoCart.Application.Common;
using NeoCart.Application.DTOs;
using NeoCart.Contracts.Authentications;
using NeoCart.Contracts.Carts.Requests;
using NeoCart.Contracts.Carts.Responses;
using NeoCart.Contracts.Common;
using NeoCart.Contracts.Orders.Responses;
using NeoCart.Contracts.Products.Requests;
using NeoCart.Contracts.Products.Responses;
using NeoCart.Contracts.Reviews.Requests;
using NeoCart.Contracts.Reviews.Responses;
using NeoCart.Domain.Entities;

namespace NeoCart.Api.Mapping;

public static class ContractMapping
{
    public static Product ToProduct(this CreateProductRequest request, Guid sellerId)
    {
        return new Product
        {
            Id = Guid.Empty,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            SellerId = sellerId
        };
    }

    public static Product ToProduct(this UpdateProductRequest request, Guid productId)
    {
        return new Product
        {
            Id = productId,
            Name = request.Name ?? string.Empty,
            Description = request.Description,
            Price = request.Price ?? 0,
            SellerId = Guid.Empty
        };
    }

    public static Review ToReview(this AddReviewRequest request, Guid productId, Guid userId)
    {
        return new Review
        {
            Id = Guid.Empty,
            Rating = request.Rating,
            ProductId = productId,
            UserId = userId,
            Comment = request.Comment
        };
    }

    public static Review ToReview(this EditReviewRequest request, Guid reviewId, Guid userId)
    {
        return new Review
        {
            Id = reviewId,
            Rating = request.Rating.GetValueOrDefault(0),
            ProductId = Guid.Empty,
            UserId = userId,
            Comment = request.Comment
        };
    }

    public static CartItem ToCartItem(this AddCartItemRequest request, Guid userId)
    {
        return new CartItem
        {
            Id = Guid.Empty,
            Quantity = request.Quantity,
            ProductId = request.ProductId,
            UserId = userId,
        };
    }

    public static RegisterDto ToRegisterDto(this RegisterRequest request)
    {
        return new RegisterDto
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password,
            Role = request.Role
        };
    }

    public static SignInDto ToSignInDto(this SignInRequest request)
    {
        return new SignInDto
        {
            Email = request.Email,
            Password = request.Password,
            RememberMe = request.RememberMe
        };
    }


    public static ProductResponse ToResponse(this Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            SellerId = product.SellerId,
            Name = product.Name,
            Description = product.Description,
            Rating = product.Rating,
            TotalRatings = product.TotalRatings,
            Price = product.Price,
            Reviews = product.Reviews?.Select(ToResponse),
            DateCreated = product.DateCreated
        };
    }

    public static ProductsResponse ToResponse(this IEnumerable<Product> products, PaginationRequest paginationRequest)
    {
        return new ProductsResponse
        {
            Items = products.Select(ToResponse),
            PageNumber = paginationRequest.PageNumber,
            PageSize = paginationRequest.PageSize
        };
    }

    public static ReviewResponse ToResponse(this Review review)
    {
        return new ReviewResponse
        {
            Id = review.Id,
            ProductId = review.ProductId,
            UserId = review.UserId,
            Rating = review.Rating,
            Comment = review.Comment,
            DateCreated = review.DateCreated,
        };
    }

    public static ReviewsResponse ToResponse(this IEnumerable<Review> reviews, PaginationRequest paginationRequest)
    {
        return new ReviewsResponse
        {
            Items = reviews.Select(ToResponse),
            PageNumber = paginationRequest.PageNumber,
            PageSize = paginationRequest.PageSize
        };
    }

    public static CartItemResponse ToResponse(this CartItem cartItem)
    {
        return new CartItemResponse
        {
            Id = cartItem.Id,
            Quantity = cartItem.Quantity,
            Product = cartItem.Product!.ToResponse(),
        };
    }

    public static CartResponse ToResponse(this IEnumerable<CartItem> cartItems, PaginationRequest paginationRequest)
    {
        return new CartResponse
        {
            Items = cartItems.Select(ToResponse),
            PageNumber = paginationRequest.PageNumber,
            PageSize = paginationRequest.PageSize
        };
    }

    public static OrderItemReponse ToResponse(this OrderItem orderItem)
    {
        return new OrderItemReponse
        {
            Id = orderItem.Id,
            OrderId = orderItem.OrderId,
            ProductId = orderItem.ProductId,
            Quantity = orderItem.Quantity,
            Price = orderItem.Price
        };
    }

    public static OrderResponse ToResponse(this Order order)
    {
        return new OrderResponse
        {
            Id = order.Id,
            UserId = order.UserId,
            TotalPrice = order.TotalPrice,
            Status = order.Status,
            Items = order.OrderItems.Select(ToResponse)
        };
    }

    public static OrderResponses ToResponse(this IEnumerable<Order> orders, PaginationRequest paginationRequest)
    {
        return new OrderResponses
        {
            Items = orders.Select(ToResponse),
            PageNumber = paginationRequest.PageNumber,
            PageSize = paginationRequest.PageSize
        };
    }


    public static GetAllProductsOptions MapToOptions(this GetAllProductsRequest request,
        PaginationRequest paginationRequest)
    {
        return new GetAllProductsOptions
        {
            Name = request.Name,
            Year = request.Year,
            SellerId = request.SellerId,
            SortField = request.SortBy?.TrimStart('-', '+'),
            SortOrder = request.SortBy is null ? SortOrder.Unsorted :
                request.SortBy.StartsWith('-') ? SortOrder.Descending : SortOrder.Ascending,

            PageNumber = paginationRequest.PageNumber,
            PageSize = paginationRequest.PageSize
        };
    }
}