using Common;
using Domain;
using Task = System.Threading.Tasks.Task;

namespace Persistence;

public class PinnedTaskRepository : IPinnedTaskRepository
{
    private readonly DatabaseContext _dbContext;
    
    public PinnedTaskRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateAsync(UserContext userContext, Guid taskId)
    {
        _dbContext.Add(new PinnedTask()
        {
            TaskId = taskId,
            UserId = userContext.UserId
        });

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(UserContext userContext, Guid taskId)
    {
        _dbContext.Remove(new PinnedTask()
        {
            TaskId = taskId,
            UserId = userContext.UserId
        });

        await _dbContext.SaveChangesAsync();
    }
}