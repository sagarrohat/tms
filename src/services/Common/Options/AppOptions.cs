namespace Common;

public class AppOptions
{
    public const string Name = "AppOptions";
    public JwtOptions JwtOptions { get; set; } = null!;
    public QueueOptions QueueOptions { get; set; } = null!;
    public DatabaseOptions DatabaseOptions { get; set; } = null!;
    public string HangfireMachineName { get; set; } = null!;
    public string FirebaseAdminPrivateKeyFile { get; set; } = null!;
    public string[] CorsAllowedOrigins { get; set; } = null!;
}

public class JwtOptions
{
    public int Expiry { get; set; }
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
}

public class QueueOptions
{
    public string Host { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string NotificationQueueId { get; set; } = string.Empty;
}

public class DatabaseOptions
{
    public string ConnectionString { get; set; } = string.Empty;
}