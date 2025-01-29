using CarRent.Domain.Reservations;
using CarRent.Domain.Shared;

namespace CarRent.Domain.Vehicle;

public sealed class Vehicle : AggregateRoot
{
    public VehicleType Type { get; set; }
    public string Brand { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Reservation> Reservations { get; set; }
}