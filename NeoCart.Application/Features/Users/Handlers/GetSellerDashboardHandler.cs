using MediatR;
using NeoCart.Application.Abstractions;
using NeoCart.Application.DTOs;
using NeoCart.Application.Features.Users.Queries;

namespace NeoCart.Application.Features.Users.Handlers;

public class GetSellerDashboardHandler : IRequestHandler<GetSellerDashboardQuery, SellerDashboardDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IReviewRepository _reviewRepository;

    public GetSellerDashboardHandler(IOrderRepository orderRepository, IProductRepository productRepository,
        IReviewRepository reviewRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _reviewRepository = reviewRepository;
    }

    public async Task<SellerDashboardDto> Handle(GetSellerDashboardQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllProductsAsync();

        var orderItems =
            (await _orderRepository.GetAllOrderItemsAsync()).Where(item => item.SellerId == request.SellerId);

        var reviews =
            (await _reviewRepository.GetAllReviewsAsync(true)).Where(item =>
                item.Product!.SellerId == request.SellerId);


        var totalOrders = orderItems
            .Select(oi => oi.OrderId)
            .Distinct()
            .Count();

        var totalProductsSold = orderItems
            .Sum(item => item.Quantity);

        var totalProductsListed = products.Count(item => item.SellerId == request.SellerId);


        var totalReviews = reviews.Count();

        var reviewScores = reviews
            .Sum(review => review.Rating);

        var sellerRating = (decimal)reviewScores / (totalReviews > 0 ? totalReviews : 1);

        var bestSellingProducts = products
            .Select(product => new TopSellingProductDto
            {
                ProductId = product.Id,
                TotalQuantitySold = product.TotalSold,
                ProductName = product.Name,
                TotalRevenue = Math.Round(product.Price * product.TotalSold, 2, MidpointRounding.AwayFromZero),
            })
            .OrderByDescending(p => p.TotalQuantitySold)
            .Take(5).AsEnumerable();


        var sellerOrders = orderItems
            .GroupBy(orderItem => orderItem.OrderId)
            .Select(order => order.Sum(orderItem => orderItem.Quantity * orderItem.Price))
            .ToList();

        var averageOrderValue = sellerOrders.Count != 0 ? sellerOrders.Average() : 0;


        return new SellerDashboardDto
        {
            TotalOrders = totalOrders,
            TotalProductsSold = totalProductsSold,
            TotalProductsListed = totalProductsListed,
            TotalReviews = totalReviews,
            BestSellingProducts = bestSellingProducts,
            SellerRating = Math.Round(sellerRating, 1, MidpointRounding.AwayFromZero),
            AverageOrderValue = Math.Round(averageOrderValue, 2, MidpointRounding.AwayFromZero)
        };
    }
}