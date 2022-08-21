namespace Common;

public class AppOptions
{
    public int JwtExpiry { get; set; }
    public string JwtIssuer { get; set; } = string.Empty;
    public string JwtAudience { get; set; } = string.Empty;
    public string JwtKey { get; set; } = string.Empty;
}

