using CarRent.Application.Abstractions.Authentication;
using CarRent.Application.Abstractions.Data;
using CarRent.Domain.Vehicles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Vehicles.Commands.Handlers;

internal sealed class CreateVehicleCommandHandler(IAppDbContext context, IUserContext userContext) : IRequestHandler<CreateVehicleCommand, Guid>
{
    public async Task<Guid> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == userContext.UserId, cancellationToken);

        if (user is null)
        {
            throw new ApplicationException($"User with id {userContext.UserId} not found");
        }
        
        if (!user.Admin)
        {
             throw new ApplicationException($"User with id {userContext.UserId} has not admin rights");           
        }

        var vehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            Type = request.Type,
            Description = request.Description,
            Brand = request.Brand,
            Name = request.Name,
            IsReserved = false
        };
        
        vehicle.AddEvent(new VehicleCreatedEvent(vehicle.Id));
        await context.Vehicles.AddAsync(vehicle, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return vehicle.Id;
    }
}