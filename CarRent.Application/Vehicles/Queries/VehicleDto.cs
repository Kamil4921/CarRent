using CarRent.Domain.Reservations;
using CarRent.Domain.Vehicles;

namespace CarRent.Application.Vehicles.Queries;

public sealed record VehicleDto
{
    public Guid Id { get; init; }
    public VehicleType Type { get; init; }
    public string Brand { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public bool IsReserved { get; init; }
    
    public ICollection<Reservation> Reservations { get; init; }
}