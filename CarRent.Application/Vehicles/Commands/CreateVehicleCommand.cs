using CarRent.Domain.Vehicles;
using MediatR;

namespace CarRent.Application.Vehicles.Commands;

public sealed record CreateVehicleCommand(VehicleType Type, string Brand, string Name, string Description) : IRequest<Guid>;