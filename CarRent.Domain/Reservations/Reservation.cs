using CarRent.Domain.Shared;

namespace CarRent.Domain.Reservations;

public class Reservation : AggregateRoot
{
    public Guid VehicleId { get; set; }
    public Guid UserId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsReserved { get; set; }
}