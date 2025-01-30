using MediatR;

namespace CarRent.Application.Users.Commands;

public sealed record LoginUserCommand(string Email, string Password) : IRequest<string>;