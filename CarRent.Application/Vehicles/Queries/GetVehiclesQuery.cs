using CarRent.Domain.Vehicles;
using MediatR;

namespace CarRent.Application.Vehicles.Queries;

public sealed record GetVehiclesQuery : IRequest<IEnumerable<VehicleDto>>;