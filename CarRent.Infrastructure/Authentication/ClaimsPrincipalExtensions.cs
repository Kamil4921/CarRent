using System.Security.Claims;

namespace CarRent.Infrastructure.Authentication;

internal static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        var userId = principal?.FindFirst(ClaimTypes.NameIdentifier).Value;
        
        return Guid.TryParse(userId, out var parsedUserId) ?
            parsedUserId :
            throw new ApplicationException("User id is unavailable");
    }
}