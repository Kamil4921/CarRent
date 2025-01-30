using CarRent.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace CarRent.Infrastructure.Authentication;

internal sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public Guid UserId =>
        _httpContextAccessor
            .HttpContext
            .User
            .GetUserId();
}