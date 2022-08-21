using Common;
using Domain;
using Persistence;

namespace Application;

public class TasksByFiltersQuery : ITasksByFiltersQuery
{
    private readonly ITaskRepository _taskRepository;
    
    public TasksByFiltersQuery(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<ICollection<TaskResponse>> ExecuteAsync(UserContext userContext, string? assignedUserId, DateTime? from, DateTime? to, string? keyword)
    {
        return await _taskRepository.GetAllAsync(userContext, assignedUserId, from, to, keyword);
    }
}