using CarRent.Application.Abstractions.Authentication;
using CarRent.Application.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Users.Commands.Handlers;

internal sealed class LoginUserCommandHandler(
    IAppDbContext context, 
    IPasswordHasher passwordHasher, 
    ITokenProvider tokenProvider) : IRequestHandler<LoginUserCommand, string>
{
    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u=>u.Email == request.Email, cancellationToken);

        if (user is null)
        {
            throw new ArgumentException("Invalid email");
        }
        
        var verifiedPassword = passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!verifiedPassword)
        {
            throw new ArgumentException("Invalid password");
        }

        var token = tokenProvider.Create(user);

        return token;
    }
}