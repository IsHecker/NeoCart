using MediatR;
using NeoCart.Application.DTOs;

namespace NeoCart.Application.Features.Users.Queries;

public record GetUserProfileQuery(Guid UserId) : IRequest<UserProfileDto?>;