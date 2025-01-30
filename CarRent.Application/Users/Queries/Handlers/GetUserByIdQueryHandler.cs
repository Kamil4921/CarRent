using CarRent.Application.Abstractions.Authentication;
using CarRent.Application.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Users.Queries.Handlers;

public class GetUserByIdQueryHandler(IAppDbContext context, IUserContext userContext) : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.UserId != userContext.UserId)
        {
            throw new ApplicationException("Invalid user id");
        }

        var user = await context.Users
            .Where(u => u.Id == request.UserId)
            .Select(u => new UserDto
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            throw new ApplicationException("User not found");
        }
        
        return user;
    }
}