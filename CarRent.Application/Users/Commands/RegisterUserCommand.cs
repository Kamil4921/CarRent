using MediatR;

namespace CarRent.Application.Users.Commands;

public sealed record RegisterUserCommand(string Email, string PhoneNumber, string FirstName, string LastName, string Password) : IRequest<Guid>;