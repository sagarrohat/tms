using Application;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public static class DashboardEndpoint
{
    public static async Task<DashboardResponse> GetDashboardTasks(HttpContext httpContext,
        [FromServices] IDashboardQuery query, string? assignedUserId)
    {
        return await query.ExecuteAsync(assignedUserId);
    }
}