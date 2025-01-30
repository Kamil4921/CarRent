using CarRent.Application.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Reservations.Queries.Handlers;

public class GetReservationsForVehicleQueryHandler(IAppDbContext context) : IRequestHandler<GetReservationsForVehicleQuery, IEnumerable<ReservationDto>>
{
    public async Task<IEnumerable<ReservationDto>> Handle(GetReservationsForVehicleQuery request, CancellationToken cancellationToken)
    {
        var reservations = await context.Reservations
            .Where(r => r.VehicleId == request.VehicleId)
            .Select(r => new ReservationDto
            {
                EndDate = r.EndDate,
                StartDate = r.StartDate
            })
            .ToListAsync(cancellationToken);
        
        return reservations;
    }
}