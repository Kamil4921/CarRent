using CarRent.Application.Vehicles.Commands;
using MediatR;

namespace CarRent.WebApi.Endpoints.Vehicles;

public sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("vehicles/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new DeleteVehicleCommand(id);
                
                var result = await sender.Send(command, cancellationToken);
                
                return Results.Ok(result);
            })
        .RequireAuthorization();
    }
}