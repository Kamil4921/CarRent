using CarRent.Domain.Shared;
using CarRent.Domain.Users;
using CarRent.Domain.Vehicles;

namespace CarRent.Domain.Reservations;

public sealed class Reservation : AggregateRoot
{
    public Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}