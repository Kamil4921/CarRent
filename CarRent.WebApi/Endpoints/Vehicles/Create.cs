using CarRent.Application.Vehicles.Commands;
using CarRent.Domain.Vehicles;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace CarRent.WebApi.Endpoints.Vehicles;

public sealed class Create : IEndpoint
{
    [SwaggerSchema("CreateVehicleRequest")]
    public sealed record Request(VehicleType Type, string Brand, string Name, string Description);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("vehicles/create", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateVehicleCommand
            (
                request.Type,
                request.Brand,
                request.Name,
                request.Description);
            
            var result = await sender.Send(command, cancellationToken);

            return Results.Created("Created", result);
        })
        .RequireAuthorization();
    }
}