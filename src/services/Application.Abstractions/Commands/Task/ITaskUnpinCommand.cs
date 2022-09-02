using Domain;
using Task = System.Threading.Tasks.Task;

namespace Application;

public interface ITaskUnpinCommand
{
    public Task ExecuteAsync(UserContext userContext, Guid id);
}