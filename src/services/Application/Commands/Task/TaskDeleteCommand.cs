using Common;
using Persistence;

namespace Application;

public class TaskDeleteCommand : ITaskDeleteCommand
{
    private readonly IRepositoryFactory _repositoryFactory;

    public TaskDeleteCommand(IRepositoryFactory repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
    }

    public async Task ExecuteAsync(Guid id)
    {
        ValidateTaskId(id);

        await _repositoryFactory.TaskRepository.DeleteAsync(id);
    }

    private static void ValidateTaskId(Guid id)
    {
        if (id == Guid.Empty)
            throw new AppException(ErrorCodes.BadRequest, string.Format(ErrorMessages.Required, nameof(id)));
    }
}