using System.Text;
using CarRent.Application.Abstractions.Authentication;
using CarRent.Application.Abstractions.Data;
using CarRent.Domain.Users;
using CarRent.Infrastructure.Authentication;
using CarRent.Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CarRent.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<AppDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default)));

        services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());

        // Authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!)),
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenProvider, TokenProvider>();

        SeedData(services);
        
        return services;
    }

    private static void SeedData(IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var scopedServices = scope.ServiceProvider;
        var context = scopedServices.GetRequiredService<AppDbContext>();

        var user = context.Users.SingleOrDefault(u => u.Email == "admin@carrent.com");
        if (user is null)
        {
            context.Users.Add(
                new User
                {
                    Email = "admin@carrent.com", 
                    PhoneNumber = "123-456-7890",
                    FirstName = "Admin",
                    LastName = "User",
                    Admin = true,
                    PasswordHash = new PasswordHasher().Hash("password")
                }
            );
        }
        
        context.SaveChanges();
    }
}
