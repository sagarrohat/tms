using Domain;
using Persistence;

namespace Application;

public class TasksByFiltersQuery : ITasksByFiltersQuery
{
    private readonly IRepositoryFactory _repositoryFactory;

    public TasksByFiltersQuery(IRepositoryFactory repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
    }

    public async Task<ICollection<TaskResponse>> ExecuteAsync(UserContext userContext, string? assignedUserId,
        DateTime? from, DateTime? to, string? keyword)
    {
        return await _repositoryFactory.TaskRepository.GetAllAsync(userContext, assignedUserId, from, to, keyword);
    }
}