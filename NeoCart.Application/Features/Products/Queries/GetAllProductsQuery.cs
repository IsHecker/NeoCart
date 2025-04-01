using MediatR;
using NeoCart.Application.Common;
using NeoCart.Application.DTOs;
using NeoCart.Application.DTOs.FilterOptions;

// ReSharper disable once CheckNamespace
namespace NeoCart.Application.Features.Product.Queries;

// Should add filters as parameter.
public record GetAllProductsQuery(ProductsFilter Filter, PaginationParams PaginationParams) : IRequest<IEnumerable<Domain.Entities.Product>>;