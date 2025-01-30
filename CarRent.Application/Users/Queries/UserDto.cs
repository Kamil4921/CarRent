namespace CarRent.Application.Users.Queries;

public sealed record UserDto
{
    public Guid Id { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
}