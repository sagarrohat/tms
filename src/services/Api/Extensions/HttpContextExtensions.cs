using Application;
using Domain;

namespace Api;

public static class HttpContextExtensions
{
    public static UserContext GetUserContext(this HttpContext httpContext)
    {
        var claimsPrincipal = httpContext.User;
        var userId = Guid.Empty;

        foreach (var claim in claimsPrincipal.Claims)
        {
            var claimType = claim.Type;

            if (string.Compare(ClaimNames.UserId, claimType, StringComparison.Ordinal) == 0)
            {
                userId = new Guid(claim.Value);
            }
        }

        return new UserContext(userId);
    }
}