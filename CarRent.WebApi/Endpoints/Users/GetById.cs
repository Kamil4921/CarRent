
using CarRent.Application.Users.Queries;
using MediatR;

namespace CarRent.WebApi.Endpoints.Users;

public sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{userId}", async (Guid userId, ISender sender, CancellationToken cancellationToken) => 
        {
            var query = new GetUserByIdQuery(userId);

            var result = await sender.Send(query, cancellationToken);

            return result;
        })
        .RequireAuthorization();
    }
}