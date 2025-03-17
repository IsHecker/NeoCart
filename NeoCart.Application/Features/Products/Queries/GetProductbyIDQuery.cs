using MediatR;

namespace NeoCart.Application.Features.Products.Queries;

public record GetProductbyIDQuery(Guid Id) : IRequest<Domain.Entities.Product>;