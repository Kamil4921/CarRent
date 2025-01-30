using CarRent.Application.Abstractions.Authentication;
using CarRent.Application.Abstractions.Data;
using CarRent.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Users.Commands.Handlers;

internal sealed class RegisterUserCommandHandler(
    IAppDbContext context,
    IPasswordHasher passwordHasher) : IRequestHandler<RegisterUserCommand, Guid>
{
    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
        {
            throw new ArgumentException("Email already exists");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = passwordHasher.Hash(request.Password),
            Admin = false
        };

        user.AddEvent(new UserRegisteredEvent(user.Id));
        
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return user.Id;
    }
}