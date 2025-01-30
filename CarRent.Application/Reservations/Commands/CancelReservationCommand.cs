using MediatR;

namespace CarRent.Application.Reservations.Commands;

public sealed record CancelReservationCommand(Guid Id) : IRequest<Guid>;