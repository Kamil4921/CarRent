using CarRent.Application.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Vehicles.Queries.Handlers;

internal sealed class GetVehicleByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetVehicleByIdQuery, VehicleDto>
{
    public async Task<VehicleDto> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        var vehicle = await context.Vehicles
            .Where(v => v.Id == request.Id)
            .Select(v => new VehicleDto
            {
                Id = v.Id,
                Brand = v.Brand,
                Name = v.Name,
                Type = v.Type,
                Description = v.Description,
                IsReserved = v.IsReserved
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (vehicle is null)
        {
            throw new ArgumentException("Vehicle not found", nameof(request));
        }
        
        return vehicle;
    }
}