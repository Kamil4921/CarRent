using CarRent.Application.Reservations.Commands;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace CarRent.WebApi.Endpoints.Reservations;

public sealed class Create : IEndpoint
{
    [SwaggerSchema("CreateReservationRequest")]
    public sealed record Request(Guid VehicleId, DateTime StartDate, DateTime EndDate);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("reservation", async (Request request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new CreateReservationCommand(
                    request.VehicleId,
                    request.StartDate,
                    request.EndDate);
                
                var result = await sender.Send(command, cancellationToken);
                
                return result;
            })
            .RequireAuthorization();
    }
}