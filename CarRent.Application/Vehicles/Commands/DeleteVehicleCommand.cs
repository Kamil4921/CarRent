using CarRent.Domain.Vehicles;
using MediatR;

namespace CarRent.Application.Vehicles.Commands;

public sealed record DeleteVehicleCommand(Guid Id) : IRequest<Guid>;