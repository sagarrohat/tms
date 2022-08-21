using Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Service;
using System.Text;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using AppContext = System.AppContext;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

services.Configure<AppOptions>(configuration.GetSection("AppOptions"));

services.AddCors();

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
        ValidIssuer = configuration["AppOptions:JwtIssuer"],
        ValidAudience = configuration["AppOptions:JwtAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppOptions:JwtKey"]))
    };
});

services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
});

services.AddAuthorization();

var connectionString = configuration["AppOptions:ConnectionString"];
services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString: connectionString));

services.ConfigureServices();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.ConfigureCors(configuration);

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.ConfigureEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.Run();