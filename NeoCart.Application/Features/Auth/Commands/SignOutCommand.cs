using MediatR;

namespace NeoCart.Application.Features.Auth.Commands;

public record SignOutCommand() : IRequest;