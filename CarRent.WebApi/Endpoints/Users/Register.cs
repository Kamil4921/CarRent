using CarRent.Application.Users.Commands;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace CarRent.WebApi.Endpoints.Users;

public sealed class Register : IEndpoint
{
    [SwaggerSchema("RegisterRequest")]
    public sealed record Request(string Email, string PhoneNumber, string FirstName, string LastName, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new RegisterUserCommand(
                request.Email,
                request.PhoneNumber,
                request.FirstName,
                request.LastName,
                request.Password);
            
            var result = await sender.Send(command, cancellationToken);
            
            return Results.Ok(result);
        });
    }
}