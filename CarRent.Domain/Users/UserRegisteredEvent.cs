using CarRent.Domain.Shared;

namespace CarRent.Domain.Users;

public record UserRegisteredEvent(Guid UserId) : IDomainEvent;