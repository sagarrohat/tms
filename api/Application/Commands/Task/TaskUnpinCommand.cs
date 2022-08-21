using Common;
using Persistence;

namespace Application;

public class TaskUnpinCommand : ITaskUnpinCommand
{
    private readonly IPinnedTaskRepository _pinnedTaskRepository;
    
    public TaskUnpinCommand(IPinnedTaskRepository pinnedTaskRepository)
    {
        _pinnedTaskRepository = pinnedTaskRepository;
    }
    
    public async Task ExecuteAsync(UserContext userContext, Guid id)
    {
        await _pinnedTaskRepository.DeleteAsync(userContext, id);
    }
}