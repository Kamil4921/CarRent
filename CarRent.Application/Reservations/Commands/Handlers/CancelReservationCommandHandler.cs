using CarRent.Application.Abstractions.Authentication;
using CarRent.Application.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Reservations.Commands.Handlers;

internal sealed class CancelReservationCommandHandler(IAppDbContext context, IUserContext userContext) : IRequestHandler<CancelReservationCommand, Guid>
{
    public async Task<Guid> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await context.Reservations.SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
        
        if (reservation is null)
        {
            throw new ApplicationException($"Reservation with id {request.Id} not found");
        }

        if (reservation.UserId != userContext.UserId)
        {
            throw new ApplicationException("You cannot cancel this reservation");
        }
        
        context.Reservations.Remove(reservation);
        await context.SaveChangesAsync(cancellationToken);
        
        return reservation.Id;
    }
}