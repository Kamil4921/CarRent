using CarRent.Domain.Shared;

namespace CarRent.Domain.Vehicle;

public record VehicleUpdatedEvent(Guid VehicleId) :IDomainEvent;