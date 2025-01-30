using CarRent.Application.Vehicles.Queries;
using MediatR;

namespace CarRent.WebApi.Endpoints.Vehicles;

public sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("vehicles", async (ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetVehiclesQuery();
                
                var result = await sender.Send(query, cancellationToken);
                
                return result;
            })
            .RequireAuthorization();
    }
}