using Application;
using Persistence;

namespace Service;

public static class ServiceCollectionExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IHashingService, HashingService>();
        services.AddSingleton<IJwtProvider, JwtProvider>();

        #region Repositories
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IPinnedTaskRepository, PinnedTaskRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        #endregion

        #region Dashboard
        services.AddScoped<IDashboardQuery, DashboardQuery>();
        #endregion

        #region Task
        services.AddScoped<ITaskCreateCommand, TaskCreateCommand>();
        services.AddScoped<ITaskByIdQuery, TaskByIdQuery>();
        services.AddScoped<ITasksByFiltersQuery, TasksByFiltersQuery>();
        services.AddScoped<ITaskUpdateCommand, TaskUpdateCommand>();
        services.AddScoped<ITaskDeleteCommand, TaskDeleteCommand>();
        #endregion
        
        services.AddScoped<ITaskPinCommand, TaskPinCommand>();
        services.AddScoped<ITaskUnpinCommand, TaskUnpinCommand>();
        
        services.AddScoped<ILoginCommand, LoginCommand>();

        services.AddScoped<IUsersQuery, UsersQuery>();
    }
}