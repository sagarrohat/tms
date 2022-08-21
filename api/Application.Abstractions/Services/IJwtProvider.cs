namespace Application;

public static class ClaimNames
{
    public const string UserId = "UserId";
    public const string FirstName = "FirstName";
    public const string LastName = "LastName";
    public const string EmailAddress = "EmailAddress";
}

public class JwtRequest
{
    public Guid UserId { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string EmailAddress { get; init; } = string.Empty;
}

public class JwtResponse
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public DateTime Expiry { get; set; }
}

public interface IJwtProvider
{
    JwtResponse GetJwt(JwtRequest request);
}
