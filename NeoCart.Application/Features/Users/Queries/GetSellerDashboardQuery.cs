using MediatR;
using NeoCart.Application.DTOs;

namespace NeoCart.Application.Features.Users.Queries;

public record GetSellerDashboardQuery(Guid SellerId) : IRequest<SellerDashboardDto>;