using Common;
using Hangfire;
using Hangfire.PostgreSql;
using ReminderProcessor;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var appOptionsConfigurationSection = builder.Configuration.GetSection(AppOptions.Name);
var appOptions = appOptionsConfigurationSection.Get<AppOptions>();
services.Configure<AppOptions>(appOptionsConfigurationSection);

var connectionString = appOptions.DatabaseOptions.ConnectionString;

services.AddScoped<IReminderJob, ReminderJob>();

services.AddHangfire(options => options.UsePostgreSqlStorage(connectionString));

// Processing Server: One that is responsible for starting the Hangfire server that will execute the background jobs
services.AddHangfireServer(options =>
    options.ServerName = $"ReminderProcessor - {appOptions.HangfireMachineName}");

var app = builder.Build();

app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<IReminderJob>("ReminderJob", x => x.Execute(), Cron.Minutely);

app.Run();