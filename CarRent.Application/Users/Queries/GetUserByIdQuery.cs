using MediatR;

namespace CarRent.Application.Users.Queries;

public sealed record GetUserByIdQuery(Guid UserId) : IRequest<UserDto>;