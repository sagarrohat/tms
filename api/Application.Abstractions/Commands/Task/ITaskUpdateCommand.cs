using Common;
using Domain;
using Task = System.Threading.Tasks.Task;

namespace Application;

public interface ITaskUpdateCommand
{
    public Task ExecuteAsync(UserContext userContext, TaskUpdateRequest request);
}
