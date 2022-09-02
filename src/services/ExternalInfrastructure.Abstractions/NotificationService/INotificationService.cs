namespace ExternalInfrastructure;

public interface INotificationService
{
    Task SendAsync(string userToken, string title, string body);
}