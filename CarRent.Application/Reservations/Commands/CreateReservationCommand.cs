using MediatR;

namespace CarRent.Application.Reservations.Commands;

public sealed record CreateReservationCommand(Guid VehicleId, DateTime StartDate, DateTime EndDate)
    : IRequest<Guid>;