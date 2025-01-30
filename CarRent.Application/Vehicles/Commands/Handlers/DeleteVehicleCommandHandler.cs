using CarRent.Application.Abstractions.Authentication;
using CarRent.Application.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Vehicles.Commands.Handlers;

internal sealed class DeleteVehicleCommandHandler(IAppDbContext context, IUserContext userContext) : IRequestHandler<DeleteVehicleCommand, Guid>
{
    public async Task<Guid> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Id == userContext.UserId);
        
        if (user is null)
        {
            throw new ApplicationException($"User with id {userContext.UserId} not found");
        }
        
        if (!user.Admin)
        {
            throw new ApplicationException($"User with id {userContext.UserId} has not admin rights");           
        }
        
        var vehicle = await context.Vehicles.SingleOrDefaultAsync(v => v.Id == request.Id, cancellationToken);

        if (vehicle is null)
        {
            throw new ApplicationException($"Vehicle with id {request.Id} not found");
        }
        
        context.Vehicles.Remove(vehicle);
        await context.SaveChangesAsync(cancellationToken);
        
        return vehicle.Id;
    }
}