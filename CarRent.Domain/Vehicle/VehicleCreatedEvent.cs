using CarRent.Domain.Shared;

namespace CarRent.Domain.Vehicle;

public record VehicleCreatedEvent(Guid VehicleId) : IDomainEvent;