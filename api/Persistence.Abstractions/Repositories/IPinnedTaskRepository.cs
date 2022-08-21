using Common;

namespace Persistence;

public interface IPinnedTaskRepository
{
    Task CreateAsync(UserContext userContext, Guid taskId);
    Task DeleteAsync(UserContext userContext, Guid taskId);
}