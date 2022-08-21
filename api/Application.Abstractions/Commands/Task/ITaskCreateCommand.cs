using Common;
using Domain;

namespace Application;

public interface ITaskCreateCommand
{
    public Task<Guid> ExecuteAsync(UserContext userContext, TaskCreateRequest request);
}
