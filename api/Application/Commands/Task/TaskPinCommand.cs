using Common;
using Persistence;

namespace Application;

public class TaskPinCommand : ITaskPinCommand
{
    private readonly IPinnedTaskRepository _pinnedTaskRepository;
    
    public TaskPinCommand(IPinnedTaskRepository pinnedTaskRepository)
    {
        _pinnedTaskRepository = pinnedTaskRepository;
    }
    
    public async Task ExecuteAsync(UserContext userContext, Guid id)
    {
        await _pinnedTaskRepository.CreateAsync(userContext, id);
    }
}