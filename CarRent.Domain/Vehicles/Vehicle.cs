using CarRent.Domain.Reservations;
using CarRent.Domain.Shared;

namespace CarRent.Domain.Vehicles;

public sealed class Vehicle : AggregateRoot
{
    public VehicleType Type { get; set; }
    public string Brand { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsReserved { get; set; }

    public ICollection<Reservation> Reservations { get; set; }
}