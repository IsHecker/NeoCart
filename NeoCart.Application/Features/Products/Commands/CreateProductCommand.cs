using MediatR;

namespace NeoCart.Application.Features.Products.Commands;

public record CreateProductCommand(Domain.Entities.Product Product) : IRequest<Domain.Entities.Product>;