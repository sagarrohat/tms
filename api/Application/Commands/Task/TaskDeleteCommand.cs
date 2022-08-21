using Common;
using Persistence;

namespace Application;

public class TaskDeleteCommand : ITaskDeleteCommand
{
    private readonly ITaskRepository _taskRepository;
    
    public TaskDeleteCommand(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    
    public async Task ExecuteAsync(Guid id)
    {
        ValidateTaskId(id);
        
        await _taskRepository.DeleteAsync(id);
    }

    private static void ValidateTaskId(Guid id)
    {
        if (id == Guid.Empty)
            throw new AppException(ErrorCodes.BadRequest, string.Format(ErrorMessages.Required, nameof(id)));
    }
}
