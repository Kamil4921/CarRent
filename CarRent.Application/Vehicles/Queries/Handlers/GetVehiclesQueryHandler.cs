using CarRent.Application.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Vehicles.Queries.Handlers;

internal sealed class GetVehiclesQueryHandler(IAppDbContext context) : IRequestHandler<GetVehiclesQuery, IEnumerable<VehicleDto>>
{
    public async Task<IEnumerable<VehicleDto>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
    {
        var vehicles = await context.Vehicles
            .Include(v => v.Reservations)
            .Select(v => new VehicleDto
            {
                Id = v.Id,
                Brand = v.Brand,
                Type = v.Type,
                Name = v.Name,
                Description = v.Description,
                Reservations = v.Reservations
            })
            .ToListAsync(cancellationToken);
        
        return vehicles;
    }
}