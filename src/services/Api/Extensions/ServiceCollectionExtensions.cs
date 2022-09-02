using System.Text;
using Application;
using Common;
using ExternalInfrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Persistence;

namespace Api;

public static class ServiceCollectionExtensions
{
    public static void ConfigureJwtAuthentication(this IServiceCollection services, AppOptions appOptions)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                RequireSignedTokens = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = appOptions.JwtOptions.Issuer,
                ValidAudience = appOptions.JwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appOptions.JwtOptions.Key))
            };
        });
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        #region Services

        services.AddSingleton<IHashingService, HashingService>();
        services.AddSingleton<IJwtProvider, JwtProvider>();
        services.AddSingleton<INotificationProducer, RabbitMqNotificationProducer>();

        #endregion

        #region Queries

        services.AddScoped<IDashboardQuery, DashboardQuery>();
        services.AddScoped<ITaskByIdQuery, TaskByIdQuery>();
        services.AddScoped<ITasksByFiltersQuery, TasksByFiltersQuery>();
        services.AddScoped<IUsersQuery, UsersQuery>();

        #endregion

        #region Commands

        services.AddScoped<ITaskCreateCommand, TaskCreateCommand>();
        services.AddScoped<ITaskUpdateCommand, TaskUpdateCommand>();
        services.AddScoped<ITaskDeleteCommand, TaskDeleteCommand>();
        services.AddScoped<ITaskPinCommand, TaskPinCommand>();
        services.AddScoped<ITaskUnpinCommand, TaskUnpinCommand>();
        services.AddScoped<IMessagingConfigurationCommand, MessagingConfigurationCommand>();
        services.AddScoped<ILoginCommand, LoginCommand>();

        #endregion

        #region RepositoryFactory

        services.AddScoped<IRepositoryFactory, RepositoryFactory>();

        #endregion
    }
}