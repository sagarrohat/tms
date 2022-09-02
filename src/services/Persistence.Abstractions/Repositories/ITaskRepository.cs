using Domain;
using Task = System.Threading.Tasks.Task;

namespace Persistence;

public interface ITaskRepository
{
    Task<Guid> CreateAsync(UserContext userContext, TaskCreateRequest request);
    Task DeleteAsync(Guid id);
    Task<TaskResponse?> GetAsync(UserContext userContext, Guid id);

    Task<List<TaskResponse>> GetAllAsync(UserContext userContext, string? assignedUserId, DateTime? from, DateTime? to,
        string? keyword);

    Task UpdateAsync(UserContext userContext, TaskResponse previous, TaskUpdateRequest request);
    Task<DashboardResponse> GetDashboardItemsAsync(string? assignedUserId);
}