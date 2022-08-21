using Application;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Service;

public static class UserEndpoints
{
    public static async Task<ICollection<UserResponse>> GetAllUsers(HttpContext httpContext, [FromServices] IUsersQuery usersQuery)
    {
        return await usersQuery.ExecuteAsync();
    }
}
