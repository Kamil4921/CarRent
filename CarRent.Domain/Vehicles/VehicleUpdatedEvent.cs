using CarRent.Domain.Shared;

namespace CarRent.Domain.Vehicles;

public record VehicleUpdatedEvent(Guid VehicleId) :IDomainEvent;