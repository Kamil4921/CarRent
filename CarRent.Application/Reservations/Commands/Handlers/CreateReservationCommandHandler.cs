using CarRent.Application.Abstractions.Authentication;
using CarRent.Application.Abstractions.Data;
using CarRent.Domain.Reservations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Reservations.Commands.Handlers;

internal sealed class CreateReservationCommandHandler(IAppDbContext context, IUserContext userContext) : IRequestHandler<CreateReservationCommand, Guid>
{
    public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var existingReservation = await context.Reservations
            .Where(r => r.VehicleId == request.VehicleId && r.StartDate == request.StartDate && r.EndDate == request.EndDate)
            .SingleOrDefaultAsync(cancellationToken);

        if (existingReservation is not null)
        {
            throw new ArgumentException($"Selected vehicle already rented for selected date");
        }
        
        var reservation = new Reservation
        {
            Id = Guid.NewGuid(),
            VehicleId = request.VehicleId,
            UserId = userContext.UserId,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };
        
        reservation.AddEvent(new ReservationCreatedEvent(reservation.Id));
        await context.Reservations.AddAsync(reservation, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return reservation.Id;
    }
}