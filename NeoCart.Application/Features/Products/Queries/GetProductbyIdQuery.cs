using MediatR;

namespace NeoCart.Application.Features.Products.Queries;

public record GetProductbyIdQuery(Guid Id) : IRequest<Domain.Entities.Product>;