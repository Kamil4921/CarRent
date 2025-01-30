using CarRent.Application.Reservations.Commands;
using MediatR;

namespace CarRent.WebApi.Endpoints.Reservations;

public sealed class Cancel : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("reservation/delete/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new CancelReservationCommand(id);
                
                var result = await sender.Send(command, cancellationToken);
                
                return result;
            })
            .RequireAuthorization();
    }
}