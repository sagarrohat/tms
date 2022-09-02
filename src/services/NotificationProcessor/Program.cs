using Application;
using Common;
using ExternalInfrastructure;
using Microsoft.EntityFrameworkCore;
using NotificationProcessor;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var appOptionsConfigurationSection = builder.Configuration.GetSection(AppOptions.Name);
var appOptions = appOptionsConfigurationSection.Get<AppOptions>();

services.Configure<AppOptions>(appOptionsConfigurationSection);

services.AddDbContext<DatabaseContext>(
    options => options.UseNpgsql(connectionString: appOptions.DatabaseOptions.ConnectionString),
    ServiceLifetime.Singleton);

services.AddHostedService<NotificationBackgroundService>();

services.AddSingleton<INotificationProcessorsFactory, NotificationProcessorsFactory>();
services.AddSingleton<INotificationService, FirebaseCloudMessaging>();
services.AddSingleton<INotificationConsumer, RabbitMqNotificationConsumer>();
services.AddSingleton<IRepositoryFactory, RepositoryFactory>();

var app = builder.Build();

app.Run();