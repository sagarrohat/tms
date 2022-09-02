using Domain;
using Task = System.Threading.Tasks.Task;

namespace Persistence;

public interface IPinnedTaskRepository
{
    Task CreateAsync(UserContext userContext, Guid taskId);
    Task DeleteAsync(UserContext userContext, Guid taskId);
}