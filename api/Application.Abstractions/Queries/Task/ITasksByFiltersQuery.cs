using Common;
using Domain;

namespace Application;

public interface ITasksByFiltersQuery
{
    public Task<ICollection<TaskResponse>> ExecuteAsync(UserContext userContext, string? assignedUserId, DateTime? from, DateTime? to, string? keyword);
}