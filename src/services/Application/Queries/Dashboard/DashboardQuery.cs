using Domain;
using Persistence;

namespace Application;

public class DashboardQuery : IDashboardQuery
{
    private readonly IRepositoryFactory _repositoryFactory;

    public DashboardQuery(IRepositoryFactory repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
    }

    public async Task<DashboardResponse> ExecuteAsync(string? assignedUserId)
    {
        return await _repositoryFactory.TaskRepository.GetDashboardItemsAsync(assignedUserId);
    }
}