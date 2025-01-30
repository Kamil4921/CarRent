using CarRent.Application.Reservations.Queries;
using MediatR;

namespace CarRent.WebApi.Endpoints.Reservations;

public sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("reservations/{id:guid}", async (Guid vehicleId, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetReservationsForVehicleQuery(vehicleId);
                
                var result = await sender.Send(query, cancellationToken);
                
                return result;
            })
            .RequireAuthorization();
    }
}