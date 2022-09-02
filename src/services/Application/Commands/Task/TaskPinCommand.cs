using Domain;
using Persistence;
using Task = System.Threading.Tasks.Task;

namespace Application;

public class TaskPinCommand : ITaskPinCommand
{
    private readonly IRepositoryFactory _repositoryFactory;

    public TaskPinCommand(IRepositoryFactory repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
    }

    public async Task ExecuteAsync(UserContext userContext, Guid id)
    {
        await _repositoryFactory.PinnedTaskRepository.CreateAsync(userContext, id);
    }
}