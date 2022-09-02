namespace Application;

public interface ILoginCommand
{
    Task<JwtResponse> ExecuteAsync(LoginRequest request);
}

public class LoginRequest
{
    public string EmailAddress { get; set; } = string.Empty;
    public string Pd { get; set; } = string.Empty;
}