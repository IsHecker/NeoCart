using MediatR;
using NeoCart.Application.Common;

// ReSharper disable once CheckNamespace
namespace NeoCart.Application.Features.Product.Queries;

// Should add filters as parameter.
public record GetAllProductsQuery(GetAllProductsOptions Options) : IRequest<IEnumerable<Domain.Entities.Product>>;