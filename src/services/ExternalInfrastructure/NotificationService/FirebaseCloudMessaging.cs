using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Options;

namespace ExternalInfrastructure;

public class FirebaseCloudMessaging : INotificationService
{
    public FirebaseCloudMessaging(IOptions<Common.AppOptions> options)
    {
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                options.Value.FirebaseAdminPrivateKeyFile)),
        });
    }

    public async Task SendAsync(string userToken, string title, string body)
    {
        var messaging = FirebaseMessaging.DefaultInstance;

        var message = new Message()
        {
            Notification = new Notification
            {
                Title = title,
                Body = body
            },
            Token = userToken
        };

        await messaging.SendAsync(message);
    }
}