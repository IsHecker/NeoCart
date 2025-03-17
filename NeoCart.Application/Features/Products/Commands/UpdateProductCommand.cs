using MediatR;

namespace NeoCart.Application.Features.Products.Commands;

public record UpdateProductCommand(Domain.Entities.Product Product) : IRequest<Domain.Entities.Product>;