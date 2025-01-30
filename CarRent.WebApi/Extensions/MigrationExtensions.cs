using CarRent.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CarRent.WebApi.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext =
            scope.ServiceProvider.GetRequiredService<AppDbContext>();

        dbContext.Database.Migrate();
    }
}