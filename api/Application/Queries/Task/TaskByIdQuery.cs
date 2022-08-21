using Common;
using Domain;
using Persistence;

namespace Application;

public class TaskByIdQuery : ITaskByIdQuery
{
    private readonly ITaskRepository _taskRepository;
    
    public TaskByIdQuery(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskResponse> ExecuteAsync(UserContext userContext, Guid id)
    {
        ValidateTaskId(id);
        
        var task = await _taskRepository.GetAsync(userContext, id);

        if (task is null)
            throw new AppException(ErrorCodes.NotFound, string.Format(ErrorMessages.TaskNotFound, id));

        return task;
    }

    private static void ValidateTaskId(Guid id)
    {
        if (id == Guid.Empty)
            throw new AppException(ErrorCodes.BadRequest, string.Format(ErrorMessages.Required, nameof(id)));
    }
}
