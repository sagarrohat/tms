using Domain;

namespace Application;

public interface IUsersQuery
{
    public Task<ICollection<UserResponse>> ExecuteAsync();
}