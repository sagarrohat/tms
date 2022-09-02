using Domain;
using Task = System.Threading.Tasks.Task;

namespace Persistence;

public interface IMessagingConfigurationRepository
{
    Task AddOrUpdateAsync(UserContext userContext, string token);
    Task<string?> GetAsync(Guid userId);
}