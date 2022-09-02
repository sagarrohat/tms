using Domain;
using Persistence;
using Task = System.Threading.Tasks.Task;

namespace Application;

public class TaskUnpinCommand : ITaskUnpinCommand
{
    private readonly IRepositoryFactory _repositoryFactory;

    public TaskUnpinCommand(IRepositoryFactory repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
    }

    public async Task ExecuteAsync(UserContext userContext, Guid id)
    {
        await _repositoryFactory.PinnedTaskRepository.DeleteAsync(userContext, id);
    }
}