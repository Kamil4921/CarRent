using CarRent.Domain.Shared;

namespace CarRent.Domain.Reservations;

public record ReservationCreatedEvent(Guid ReservationId) : IDomainEvent;