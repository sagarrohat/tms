using Task = System.Threading.Tasks.Task;

namespace Application;

public interface ITaskDeleteCommand
{
    public Task ExecuteAsync(Guid id);
}
