using CarRent.Domain.Reservations;
using CarRent.Domain.Users;
using CarRent.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Abstractions.Data;

public interface IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}