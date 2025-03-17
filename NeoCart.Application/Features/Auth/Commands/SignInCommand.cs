using MediatR;
using NeoCart.Application.DTOs;
using NeoCart.Application.Results;

namespace NeoCart.Application.Features.Auth.Commands;

public record SignInCommand(SignInDto SignInDto) : IRequest<SignInResult>;