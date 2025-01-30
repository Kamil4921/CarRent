using MediatR;

namespace CarRent.Application.Reservations.Queries;

public sealed record GetReservationsForVehicleQuery(Guid VehicleId) : IRequest<IEnumerable<ReservationDto>>;