using MediatR;

namespace CarRent.Application.Vehicles.Queries;

public sealed record GetVehicleByIdQuery(Guid Id) : IRequest<VehicleDto>;