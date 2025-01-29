using CarRent.Domain.Shared;

namespace CarRent.Domain.Reservations;

public record ReservationDeletedEvent(Guid ReservationId) : IDomainEvent;