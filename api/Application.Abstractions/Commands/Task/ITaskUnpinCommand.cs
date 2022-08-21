using Common;

namespace Application;

public interface ITaskUnpinCommand
{
    public Task ExecuteAsync(UserContext userContext, Guid id);
}