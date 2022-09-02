using Common;
using Domain;
using Persistence;

namespace Application;

public class TaskByIdQuery : ITaskByIdQuery
{
    private readonly IRepositoryFactory _repositoryFactory;

    public TaskByIdQuery(IRepositoryFactory repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
    }

    public async Task<TaskResponse> ExecuteAsync(UserContext userContext, Guid id)
    {
        ValidateTaskId(id);

        var task = await _repositoryFactory.TaskRepository.GetAsync(userContext, id);

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