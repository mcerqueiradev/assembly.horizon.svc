using Assembly.Horizon.Security.Interface;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Assembly.Horizon.Security.Services;

public class UserResolverService : IUserResolverService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserResolverService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal CurrentUser => _httpContextAccessor.HttpContext?.User;

    public Guid GetUserId()
    {
        var userIdClaim = CurrentUser?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return userIdClaim != null ? Guid.Parse(userIdClaim) : Guid.Empty;
    }

    public string GetUserName()
    {
        return CurrentUser?.FindFirst(ClaimTypes.Name)?.Value;
    }

    public string GetUserEmail()
    {
        return CurrentUser?.FindFirst(ClaimTypes.Email)?.Value;
    }
}