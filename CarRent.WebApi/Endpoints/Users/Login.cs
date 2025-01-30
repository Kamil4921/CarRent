using CarRent.Application.Users.Commands;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace CarRent.WebApi.Endpoints.Users;

public sealed class Login : IEndpoint
{
    [SwaggerSchema("LoginRequest")]
    public sealed record Request(string Email, string Password);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/login", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new LoginUserCommand(request.Email, request.Password);
            
            var result = await sender.Send(command, cancellationToken);
            
            return Results.Ok(result);
        });
    }
}