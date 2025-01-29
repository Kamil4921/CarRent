using CarRent.Domain.Shared;

namespace CarRent.Domain.Vehicles;

public record VehicleCreatedEvent(Guid VehicleId) : IDomainEvent;