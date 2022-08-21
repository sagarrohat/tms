using Common;
using Domain;

namespace Application;

public interface ITaskByIdQuery
{
    public Task<TaskResponse> ExecuteAsync(UserContext userContext, Guid id);
}
