using Application;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public static class UserEndpoints
{
    public static async Task<ICollection<UserResponse>> GetAllUsers(HttpContext httpContext,
        [FromServices] IUsersQuery query)
    {
        return await query.ExecuteAsync();
    }
}