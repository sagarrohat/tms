using Domain;
using Task = System.Threading.Tasks.Task;

namespace Application;

public interface IMessagingConfigurationCommand
{
    public Task ExecuteAsync(UserContext userContext, string token);
}