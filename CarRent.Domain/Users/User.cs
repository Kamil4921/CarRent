using CarRent.Domain.Reservations;
using CarRent.Domain.Shared;

namespace CarRent.Domain.Users;

public sealed class User: AggregateRoot
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PasswordHash { get; set; }

    public ICollection<Reservation> Reservations { get; set; }
}