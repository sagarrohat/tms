namespace Service;

public static class WebApplicationExtensions
{
    public static void ConfigureEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => "Welcome to Task Management API");

        app.MapPost("api/v1/login", LoginEndpoint.Login);

        app.MapGet("api/v1/users", UserEndpoints.GetAllUsers).RequireAuthorization();

        app.MapPost("api/v1/tasks", TaskEndpoints.CreateTask).RequireAuthorization();
        app.MapDelete("api/v1/tasks/{id:guid}", TaskEndpoints.DeleteTaskById).RequireAuthorization();
        app.MapGet("api/v1/tasks/{id:guid}", TaskEndpoints.GetTaskById).RequireAuthorization();
        app.MapPut("api/v1/tasks/{id:guid}", TaskEndpoints.UpdateTask).RequireAuthorization();
        app.MapGet("api/v1/tasks", TaskEndpoints.GetTasksByFilters).RequireAuthorization();

        app.MapPost("api/v1/tasks/{id:guid}/pinned", TaskEndpoints.PinTask).RequireAuthorization();
        app.MapDelete("api/v1/tasks/{id:guid}/pinned", TaskEndpoints.UnpinTask).RequireAuthorization();
        
        app.MapGet("api/v1/dashboard/tasks", DashboardEndpoint.GetDashboardTasks).RequireAuthorization();
    }

    public static void ConfigureCors(this WebApplication app, ConfigurationManager configuration)
    {
        var corsAllowedOrigins = configuration.GetSection("CorsAllowedOrigins").Get<string[]>();
        if (corsAllowedOrigins == null || corsAllowedOrigins.Length == 0 || corsAllowedOrigins.Any(x=>x == "*"))
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        else
            app.UseCors(options => options.WithOrigins(corsAllowedOrigins).AllowAnyHeader().AllowAnyMethod());
    }
}