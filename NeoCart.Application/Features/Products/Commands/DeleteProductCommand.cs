using MediatR;

namespace NeoCart.Application.Features.Products.Commands;

public record DeleteProductCommand(Guid Id) : IRequest;