using Application;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace Api;

public static class MessagingConfigurationEndpoints
{
    public static async Task AddOrUpdateMessagingConfiguration(HttpContext httpContext,
        [FromServices] IMessagingConfigurationCommand command, string token)
    {
        await command.ExecuteAsync(httpContext.GetUserContext(), token);
    }
}