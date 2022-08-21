using Domain;
using Persistence;

namespace Application;

public class DashboardQuery : IDashboardQuery
{
    private readonly ITaskRepository _taskRepository;

    public DashboardQuery(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<DashboardResponse> ExecuteAsync(string? assignedUserId)
    {
        return await _taskRepository.GetDashboardItemsAsync(assignedUserId);
    }
}
