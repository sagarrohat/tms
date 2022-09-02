using Common;
using Persistence;
using Api;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using AppContext = System.AppContext;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var appOptionsConfigurationSection = builder.Configuration.GetSection(AppOptions.Name);
var appOptions = appOptionsConfigurationSection.Get<AppOptions>();

services.Configure<AppOptions>(appOptionsConfigurationSection);
services.Configure<JsonOptions>(options => { options.SerializerOptions.PropertyNamingPolicy = null; });
services.AddCors();
services.ConfigureJwtAuthentication(appOptions);
services.AddAuthorization();
services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(connectionString: appOptions.DatabaseOptions.ConnectionString));
services.ConfigureServices();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.ConfigureCors(appOptions);
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.ConfigureEndpoints();
app.UseAuthentication();
app.UseAuthorization();

app.Run();