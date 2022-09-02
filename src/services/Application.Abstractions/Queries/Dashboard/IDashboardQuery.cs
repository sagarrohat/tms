using Domain;

namespace Application;

public interface IDashboardQuery
{
    public Task<DashboardResponse> ExecuteAsync(string? assignedUserId);
}

