using CarRent.Application.Vehicles.Queries;
using MediatR;

namespace CarRent.WebApi.Endpoints.Vehicles;

public sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("vehicle/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetVehicleByIdQuery(id);

                var result = await sender.Send(query, cancellationToken);

                return result;
            })
            .RequireAuthorization();
    }
}