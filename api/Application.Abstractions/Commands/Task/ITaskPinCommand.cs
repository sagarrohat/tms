using Common;

namespace Application;

public interface ITaskPinCommand
{
    public Task ExecuteAsync(UserContext userContext, Guid id);
}