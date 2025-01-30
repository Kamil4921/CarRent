namespace CarRent.Application.Reservations.Queries;

public sealed record ReservationDto
{
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}